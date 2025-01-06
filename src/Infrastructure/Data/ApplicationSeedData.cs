using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class ApplicationSeedData
{
    public static async Task SeedAsync(ApplicationDbContext dbContext,
        ILogger logger,
        int retry = 0)
    {
        if (dbContext.Database.IsNpgsql()) dbContext.Database.Migrate();

        var retryForAvailability = retry;
        try
        {
            if (!await dbContext.CatalogTypes.AnyAsync())
            {
                await dbContext.CatalogTypes.AddRangeAsync(GetCatalogTypes());

                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.CatalogBrands.AnyAsync())
            {
                await dbContext.CatalogBrands.AddRangeAsync(GetCatalogBrands());

                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.CatalogItems.AnyAsync())
            {
                await dbContext.CatalogItems.AddRangeAsync(GetCatalogItems());

                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;

            logger.LogError(ex, "An error occurred while seeding the database.");
            await SeedAsync(dbContext, logger, retryForAvailability);
            throw;
        }
    }

    private static IEnumerable<CatalogType> GetCatalogTypes()
    {
        return new List<CatalogType>
        {
            new() { Type = "Summer Shoes" },
            new() { Type = "Winter Boots" },
            new() { Type = "Spring Shoes" },
            new() { Type = "Autumn Shoes" },
            new() { Type = "Men's Shoes" },
            new() { Type = "Women's Shoes" },
            new() { Type = "Kids' Shoes" }
        };
    }

    private static IEnumerable<CatalogBrand> GetCatalogBrands()
    {
        return new List<CatalogBrand>
        {
            new() { Brand = "Adidas" },
            new() { Brand = "Nike" },
            new() { Brand = "Puma" },
            new() { Brand = "Crocs" },
            new() { Brand = "Asics" },
            new() { Brand = "Converse" }
        };
    }

    private static IEnumerable<CatalogItem> GetCatalogItems()
    {
        return new List<CatalogItem>
        {
            new()
            {
                Name = "Кроссовки Ad Terrex  Black S18058-02",
                Description = "Прочные и стильные кроссовки для активного отдыха.", Price = 57.56m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f54/600_600_140cd750bba9870f18aada2478b24840a/13dk3ova01n8jlzm5tiulnot7u80nt4e.jpeg",
                StockQuantity = 16, CatalogTypeId = 3, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки Ad Terrex  Black Grey S18058-26",
                Description = "Кроссовки с улучшенной поддержкой для походов по пересеченной местности.",
                Price = 57.56m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/9a4/600_600_140cd750bba9870f18aada2478b24840a/2iun6dj38v7c752lib3kwii07ghcmqzb.jpeg",
                StockQuantity = 51, CatalogTypeId = 3, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки A. Response CL Multy Violet S19024-40",
                Description = "Многофункциональные кроссовки с ярким дизайном для активного использования.",
                Price = 63.78m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/0f3/600_600_140cd750bba9870f18aada2478b24840a/asrqq3oes9vyesmdyg2883rphvjwezhs.jpeg",
                StockQuantity = 85, CatalogTypeId = 3, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки Adibreak S21007-05",
                Description = "Спортивные кроссовки с хорошей амортизацией для активных занятий.", Price = 61.78m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/fe2/600_600_140cd750bba9870f18aada2478b24840a/5x7ah32yrg7fg42wfhrd8ykqfo5u5c5m.jpeg",
                StockQuantity = 66, CatalogTypeId = 3, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки A. Climacool S19002-12",
                Description = "Кроссовки для комфортного активного использования в любую погоду.", Price = 70.67m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/d06/600_600_140cd750bba9870f18aada2478b24840a/kqdmjkci9dqajfnxxb1a3ptz6v9o4ilb.jpeg",
                StockQuantity = 62, CatalogTypeId = 3, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки Casual Sports S19010-29",
                Description = "Удобные кроссовки для повседневного ношения.", Price = 47.78m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/15c/600_600_140cd750bba9870f18aada2478b24840a/rzi6fn7vnfnwgc8dtksw3w8y1v7yls4y.jpeg",
                StockQuantity = 49, CatalogTypeId = 5, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки Nike VaporMax 2020",
                Description = "Кроссовки Nike для профессиональных спортсменов с технологией Air Max.", Price = 99.78m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/302/600_600_140cd750bba9870f18aada2478b24840a/5k4hfmwdcsuf70uyl62pcd2wrbp1vlbb.jpeg",
                StockQuantity = 34, CatalogTypeId = 5, CatalogBrandId = 1
            },
            new()
            {
                Name = "Кроссовки Nike Zoom Air",
                Description = "Стильные и комфортные кроссовки с высокой амортизацией.", Price = 86.44m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/53e/600_600_140cd750bba9870f18aada2478b24840a/xt1dlfdxbb37hoq5xl5og60c9mcwe47f.jpeg",
                StockQuantity = 45, CatalogTypeId = 5, CatalogBrandId = 1
            },
            new()
            {
                Name = "Кроссовки Puma RS-X3 Puzzle",
                Description = "Яркие и стильные кроссовки Puma для активных людей.", Price = 64.22m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/a9e/600_600_140cd750bba9870f18aada2478b24840a/59hrb1gy9y88t0wv2x76kqhzvsqbxcdp.jpeg",
                StockQuantity = 58, CatalogTypeId = 5, CatalogBrandId = 2
            },
            new()
            {
                Name = "Кроссовки Puma Future Rider",
                Description = "Кроссовки с амортизацией для удобства и комфорта на каждый день.", Price = 59.78m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/fe5/600_600_140cd750bba9870f18aada2478b24840a/0j9qdbw2i2jexwnyx6b2hdg75zqpt7hb.jpeg",
                StockQuantity = 12, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки Puma R698",
                Description = "Кроссовки с амортизацией для удобства и комфорта на каждый день.", Price = 55.33m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/703/600_600_140cd750bba9870f18aada2478b24840a/lp1jp2mmbos0bmbqa00iyldmrsm5gb6i.jpeg",
                StockQuantity = 39, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки Reebok Classic Leather",
                Description = "Классические кроссовки Reebok для повседневной носки.", Price = 50.898m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/129/600_600_140cd750bba9870f18aada2478b24840a/bfgajw40gqz1ce69oypz7iqm80p1tjx0.jpeg",
                StockQuantity = 27, CatalogTypeId = 5, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроссовки Reebok Nano X2",
                Description = "Кроссовки для активных тренировок с высокой амортизацией.", Price = 70.67m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/8b4/600_600_140cd750bba9870f18aada2478b24840a/x5h7fj4t3ltipqg28bgm32ctwpa1g2w7.jpeg",
                StockQuantity = 43, CatalogTypeId = 5, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кеды Converse Kerstschoenen high Green S21015-73",
                Description =
                    "Эти зеленые высокие кеды из коллекции Converse сочетают стиль и комфорт для повседневного использования.",
                Price = 59.1m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/0cf/600_600_140cd750bba9870f18aada2478b24840a/f1iav7hqi9tp2ny6jiu616wd6j2o8zjy.jpeg",
                StockQuantity = 16, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды Converse Kerstschoenen high Red S21015-20",
                Description =
                    "Яркие красные высокие кеды Converse, которые добавят стильности вашему образу.",
                Price = 59.1m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/696/600_600_140cd750bba9870f18aada2478b24840a/vutqosibcztig4t02ke4l05ctcqeoyfv.jpeg",
                StockQuantity = 83, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's High Chuck Taylor All Star 70 Green S21020-08",
                Description =
                    "Зеленые высокие кеды Chuck Taylor All Star 70 от Converse, отличный выбор для поклонников классического стиля.",
                Price = 39.6m,
                PictureUrl = "https://sneakertown.kz/upload/iblock/991/plyn8zv5695n34t2kwwm2kcno32g90yc.jpg",
                StockQuantity = 31, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor One Star Low Gray S21008-07",
                Description =
                    "Серые низкие кеды Converse с одной звездой, которые идеально сочетаются с любой одеждой.",
                Price = 47.4m,
                PictureUrl = "https://sneakertown.kz/upload/iblock/b1a/677v3xvdulias6q7m2itvl6mdt5yuzbl.jpeg",
                StockQuantity = 48, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды Chuck Taylor Black на меху S21011-02",
                Description = "Черные кеды Chuck Taylor с меховой подкладкой, подходящие для холодной погоды.",
                Price = 35.6m,
                PictureUrl = "https://sneakertown.kz/upload/iblock/b1a/677v3xvdulias6q7m2itvl6mdt5yuzbl.jpeg",
                StockQuantity = 36, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star Low Class A White S21002-04W",
                Description = "Белые низкие кеды Converse с классическим дизайном для любителей минимализма.",
                Price = 11.9m,
                PictureUrl = "https://sneakertown.kz/upload/iblock/b1a/677v3xvdulias6q7m2itvl6mdt5yuzbl.jpeg",
                StockQuantity = 73, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Low Chuck 70 White S21021-04",
                Description = "Низкие белые кеды Chuck 70 от Converse для тех, кто ценит стиль и удобство.",
                Price = 33.8m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/ad0/600_600_140cd750bba9870f18aada2478b24840a/1gp401x2od4s4yv11fzti4eogs3bur4d.jpg",
                StockQuantity = 98, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star Low White S21008-04",
                Description = "Белые низкие кеды Chuck Taylor All Star с классическим дизайном и стильной отделкой.",
                Price = 240.4m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/6c7/600_600_140cd750bba9870f18aada2478b24840a/wwm1dw8mul2e39z5q6rg7270jjho2uev.jpg",
                StockQuantity = 34, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's High Class a Black White S21015-01",
                Description = "Высокие кеды в черно-белом цвете с классической отделкой Converse.", Price = 39.6m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/ba2/600_600_140cd750bba9870f18aada2478b24840a/qjtm6acm2wenl2z9kpb8fbekuj53ms17.jpg",
                StockQuantity = 67, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star 70 White Low S21007-04",
                Description = "Классические белые низкие кеды Chuck Taylor All Star 70 для стильных людей.",
                Price = 35.5m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/2eb/600_600_140cd750bba9870f18aada2478b24840a/25a33g1h929bwt84b8046rf31zdeudj3.jpg",
                StockQuantity = 56, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star 70 Black White Low S21007-01",
                Description = "Черно-белые низкие кеды Converse Chuck Taylor All Star 70 для поклонников классики.",
                Price = 35.5m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/b1a/600_600_140cd750bba9870f18aada2478b24840a/677v3xvdulias6q7m2itvl6mdt5yuzbl.jpeg",
                StockQuantity = 34, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды Low Classic Black White S21005-01",
                Description = "Низкие классические черно-белые кеды Converse, подходящие для ежедневной носки.",
                Price = 21.3m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/c3f/600_600_140cd750bba9870f18aada2478b24840a/lxkq2tmvvvbedpfh76ujexo9sieona02.jpg",
                StockQuantity = 8, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star High Class A White S21015-04",
                Description = "Белые высокие кеды Chuck Taylor с классическим дизайном и удобной посадкой.",
                Price = 18.3m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/a30/600_600_140cd750bba9870f18aada2478b24840a/3ro3waa26vhziecna592h6tc3u0undq0.jpg",
                StockQuantity = 78, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Chuck Taylor All Star Low Class A White S21002-04",
                Description = "Кеды Converse с низким профилем и классическим дизайном для повседневной носки.",
                Price = 18.3m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/991/600_600_140cd750bba9870f18aada2478b24840a/plyn8zv5695n34t2kwwm2kcno32g90yc.jpg",
                StockQuantity = 15, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кеды С's Low Class A White S21001-04",
                Description = "Белые низкие кеды Converse для стильных и комфортных прогулок.", Price = 33.8m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/17d/600_600_140cd750bba9870f18aada2478b24840a/85lmsy7jjbpf0i1zuauydi4txk5tldjg.jpg",
                StockQuantity = 100, CatalogTypeId = 5, CatalogBrandId = 6
            },
            new()
            {
                Name = "Кроссовки A's GEL NYC 2055 S19109-07",
                Description = "Стильные кроссовки для активного отдыха с хорошей амортизацией.", Price = 65.56m,
                PictureUrl =
                    "https://sneakertown.kz/upload/iblock/762/u9a8mh9el88u1zjkcidyzmbaxd0ns04e.jpg",
                StockQuantity = 57, CatalogTypeId = 1, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's Gel-Contend 4 White S13007-04",
                Description = "Комфортные кроссовки для ежедневной носки с отличной поддержкой.", Price = 66.00m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/c32/600_600_140cd750bba9870f18aada2478b24840a/crtqmak0q8g6pf7eot2uke85lofbg751.jpeg",
                StockQuantity = 2, CatalogTypeId = 2, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's Gel-Flux 4 Light Beige S13007-10",
                Description = "Легкие и удобные кроссовки для активных людей, ищущих комфорт в каждый день.",
                Price = 66.00m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/6b4/600_600_140cd750bba9870f18aada2478b24840a/ibfwlkte9s0s4l270zinjfxu51zxrn9b.jpeg",
                StockQuantity = 45, CatalogTypeId = 3, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's GEL-Kahana 8 Dark Grey S13316-07",
                Description = "Прочные и надежные кроссовки для долгих прогулок и тренировок.", Price = 66.44m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/48f/600_600_140cd750bba9870f18aada2478b24840a/lek6ye8a2wpelj2qf72wabn09msdt2v0.jpeg",
                StockQuantity = 20, CatalogTypeId = 4, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's GEL-Kahana 8 White Black S13316-01",
                Description = "Кроссовки с продвинутой системой амортизации и современным дизайном.", Price = 66.44m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f3d/600_600_140cd750bba9870f18aada2478b24840a/8wr7dksicyai6b2e1f6x9fd2ls0kny38.jpeg",
                StockQuantity = 61, CatalogTypeId = 5, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's GEL-Kahana 8 Silver Dark Grey S13216-07",
                Description = "Эргономичные кроссовки для комфортных пробежек и активных тренировок.", Price = 62.00m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/c2e/600_600_140cd750bba9870f18aada2478b24840a/yrrc9mep9gwxrrhuy88l4fj1s6dfsyh9.jpeg",
                StockQuantity = 49, CatalogTypeId = 6, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's GEL - 1130 White Cloud Grey S13199-07",
                Description = "Легкие и стильные кроссовки для активных людей, которые ценят комфорт и качество.",
                Price = 64.22m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/87c/600_600_140cd750bba9870f18aada2478b24840a/3t7sybaxuzx1u7xoa9dz149wc9pijuyt.jpeg",
                StockQuantity = 92, CatalogTypeId = 7, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроссовки A's GEL Kayano 14 Black' S13008-02",
                Description = "Высокотехнологичные кроссовки с отличной амортизацией и стильным дизайном.",
                Price = 64.22m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/5c1/600_600_140cd750bba9870f18aada2478b24840a/yrrc9mep9gwxrrhuy88l4fj1s6dfsyh9.jpeg",
                StockQuantity = 30, CatalogTypeId = 1, CatalogBrandId = 5
            },
            new()
            {
                Name = "Кроксы Crocs Classic Beige L11121-13",
                Description = "Высокотехнологичные кроссовки с отличной амортизацией и стильным дизайном.",
                Price = 26.50m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f07/600_600_140cd750bba9870f18aada2478b24840a/l3c3no3j2law8xetbb8xu59ss0d68icv.jpeg",
                StockQuantity = 11, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроксы Crocs Classic White L11121-04",
                Description = "Кроксы классические в белом цвете с анатомической стелькой.", Price = 26.50m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f07/600_600_140cd750bba9870f18aada2478b24840a/l3c3no3j2law8xetbb8xu59ss0d68icv.jpeg",
                StockQuantity = 33, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроксы Crocs Salehe Bembury L11120-17",
                Description =
                    "Кроксы, разработанные в коллаборации с дизайном Salehe Bembury, отличающиеся стильным внешним видом.",
                Price = 45.58m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f07/600_600_140cd750bba9870f18aada2478b24840a/l3c3no3j2law8xetbb8xu59ss0d68icv.jpeg",
                StockQuantity = 21, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроксы Crocs White L11120-04",
                Description = "Элегантные белые кроксы с прочной подошвой и комфортной посадкой.", Price = 39.17m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/899/600_600_140cd750bba9870f18aada2478b24840a/8ed63avhtvnfjn9ig2buiopffzc2qjmx.jpeg",
                StockQuantity = 12, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроксы Crocs Echo Clog Beige L11120-09",
                Description = "Удобные кроксы Echo Clog в бежевом цвете для активного отдыха.", Price = 51.88m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/bb3/600_600_140cd750bba9870f18aada2478b24840a/hixf40uktbjtg790uij0qay1e9ragqdz.jpeg",
                StockQuantity = 8, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроксы Crocs Blue Red L11119-03",
                Description = "Стильные кроксы в синих и красных оттенках, идеальные для лета.", Price = 29.04m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f20/600_600_140cd750bba9870f18aada2478b24840a/pd1qvaudulgpmji3eyr99iguuj2e9lhg.jpeg",
                StockQuantity = 43, CatalogTypeId = 1, CatalogBrandId = 4
            },
            new()
            {
                Name = "Кроссовки P. Suede XL Pleasures Black Beige S14490-01",
                Description =
                    "Элегантные кроссовки в черно-бежевом цвете с высоким качеством материалов и современным дизайном.",
                Price = 53.3m,
                PictureUrl =
                    "https://sneakertown.kz/upload/iblock/262/xjzj537000ie520joc2pyqajvz17ltb2.jpg",
                StockQuantity = 44, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки P. Suede XL Negro Olive White S14098-02",
                Description =
                    "Кроссовки с оливково-белой расцветкой, сочетание стиля и комфорта для активного образа жизни.",
                Price = 53.7m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/f51/600_600_140cd750bba9870f18aada2478b24840a/j3wgwibil7qzce5gb4g7i63ba6eyhabz.jpeg",
                StockQuantity = 88, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки P. Suede XL Red S14091-33",
                Description =
                    "Яркие красные кроссовки с дизайнерским подходом, которые отлично подойдут для повседневного ношения.",
                Price = 53.7m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/078/600_600_140cd750bba9870f18aada2478b24840a/fps24x67mf4k15ff9cnjr6dqtxae5y2o.jpeg",
                StockQuantity = 14, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки P. Speed Cat Red S14090-20",
                Description = "Спортивные кроссовки в красном цвете, созданные для максимального комфорта и скорости.",
                Price = 54.0m,
                PictureUrl =
                    "https://sneakertown.kz/upload/iblock/9b6/pz63y77tfzonuy7quq9xocg9xirbo5nj.jpeg",
                StockQuantity = 94, CatalogTypeId = 5, CatalogBrandId = 3
            },
            new()
            {
                Name = "Кроссовки P. Suede XL Black White S14090-01",
                Description =
                    "Классические черно-белые кроссовки, которые идеально подойдут для любых стилей и ситуаций.",
                Price = 53.3m,
                PictureUrl =
                    "https://sneakertown.kz/upload/resize_cache/iblock/262/600_600_140cd750bba9870f18aada2478b24840a/xjzj537000ie520joc2pyqajvz17ltb2.jpg",
                StockQuantity = 10, CatalogTypeId = 5, CatalogBrandId = 3
            }
        };
    }
}