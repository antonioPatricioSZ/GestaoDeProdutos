using Domain.Entities;
using Domain.Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository.Repository;

public class ProductRepository : IProductWriteOnlyRepository, IProductReadOnlyRepository {

    private readonly GestaoDeProdutosContext _context;

    public ProductRepository(
        GestaoDeProdutosContext context
    ){
        _context = context;
    }

    public async Task<bool> AddProduct(Product product, long categoryId) {

        var categoryExists = await _context.Categories.FirstOrDefaultAsync(category => category.Id == categoryId); 
        if(categoryExists is not null) {
            product.CategoryId = categoryExists.Id;
            await _context.Products.AddAsync(product);
            return true;
        } else { 
            return false;
        }
    }

    public async Task Delete(long productId) {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == productId);
        _context.Products.Remove(product);
    }

    public async Task<IList<Product>> GetAllProducts() {

        var result = await _context.Products
         .AsNoTracking()
         .Include(p => p.Category) // Carrega a categoria relacionada
         .ToListAsync();

        return result;

    }

    public async Task<Product> GetProductById(long productId) {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(product => product.Id == productId);
    }
}
