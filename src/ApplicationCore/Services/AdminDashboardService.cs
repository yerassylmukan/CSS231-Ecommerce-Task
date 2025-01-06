using System.Globalization;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IApplicationDbContext _context;

    public AdminDashboardService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalesReportDTO>> SalesReportAsync(CancellationToken cancellationToken)
    {
        var salesData = await _context.Orders
            .Where(o => o.IsConfirmed)
            .SelectMany(o => o.Items)
            .GroupBy(i => new { i.Order.OrderDate.Month })
            .Select(g => new SalesReportDTO
            {
                Month = GetMonthName(g.Key.Month),
                SalesCount = g.Sum(i => i.Quantity)
            })
            .ToListAsync(cancellationToken);

        return salesData;
    }

    public async Task<IEnumerable<CustomerActivityLogDTO>> GetCustomerActivityLogsAsync(
        CancellationToken cancellationToken)
    {
        var logs = await _context.Orders
            .Select(o => new CustomerActivityLogDTO
            {
                UserId = o.UserId,
                JustOrdered = o.IsConfirmed,
                OrderDate = o.OrderDate
            })
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync(cancellationToken);

        return logs;
    }

    public async Task<IEnumerable<InventorySummaryDTO>> GetInventorySummaryAsync(CancellationToken cancellationToken)
    {
        var inventory = await _context.CatalogItems
            .Select(ci => new InventorySummaryDTO
            {
                ProductName = ci.Name,
                StockQuantity = ci.StockQuantity
            })
            .OrderBy(ci => ci.ProductName)
            .ToListAsync(cancellationToken);

        return inventory;
    }

    private static string GetMonthName(int monthNumber)
    {
        if (monthNumber < 1 || monthNumber > 12) return "Invalid month number";

        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber);
    }
}