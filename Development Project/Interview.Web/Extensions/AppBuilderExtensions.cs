using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sparcpoint.Infrastructure;
using Sparcpoint.Models;

namespace Interview.Web.Extensions;

public static class AppBuilderExtensions
{
    public static void InitializeDb(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Database.Migrate();
        Seed(context);
    }

    private static void Seed(ProductContext context)
    {
        if (context.Products.Any())
        {
            return;
        }
        
        var product = new Product
        {
            Name = "Socks",
            Description = "Ordinary linen socks",
            Attributes = new List<Attribute>
            {
                new() { Name = "Size", Description = "43-45" },
                new() { Name = "Color", Description = "Black" }
            },
            Categories = new List<Category>
            {
                new() { Name = "Clothes", Description = "Clothes" },
                new() { Name = "Sports", Description = "Sports" }
            }
        };

        context.Add(product);
        context.SaveChangesAsync().GetAwaiter().GetResult(); //I want to use the overloaded method
    }
}