using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandAsync()
    {
        return await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypeAsync()
    {
        return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetProductAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if(!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.Brand == brand);
            
        }

        if(!string.IsNullOrEmpty(type))
        {
            query = query.Where(p => p.Type == type);
        }
        Console.ForegroundColor = ConsoleColor.Yellow; // Ovo čini tekst žutim
        Console.WriteLine($"--- SORTIRANJE: {sort} ---");
        Console.ResetColor();
        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(x => x.Name)
        };

        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public bool ProductExists(int id)
    {
       return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}