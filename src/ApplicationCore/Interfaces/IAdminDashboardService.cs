using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces;

public interface IAdminDashboardService
{
    Task<IEnumerable<SalesReportDTO>> SalesReportAsync(CancellationToken cancellationToken);
    Task<IEnumerable<CustomerActivityLogDTO>> GetCustomerActivityLogsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<InventorySummaryDTO>> GetInventorySummaryAsync(CancellationToken cancellationToken);
}