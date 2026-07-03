using Core.Entities;

namespace Infrastructure.Config;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductAsync(string? brand, string? type, string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandAsync();
    Task<IReadOnlyList<string>> GetTypeAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}