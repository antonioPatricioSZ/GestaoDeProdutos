using Domain.Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository.Repository;

public class ProductRepository : 
    IProductWriteOnlyRepository,
    IProductReadOnlyRepository,
    IProductUpdateOnlyRepository {

    private readonly GestaoDeProdutosContext _context;

    public ProductRepository(
        GestaoDeProdutosContext context
    ){
        _context = context;
    }

    public async Task AddProduct(Domain.Entities.Product product) {
        await _context.Products.AddAsync(product);
    }

    public async Task Delete(long productId) {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == productId);
        _context.Products.Remove(product);
    }

    public async Task<IList<Domain.Entities.Product>> GetAllProducts() {

        var result = await _context.Products
         .AsNoTracking()
         .Include(p => p.Category) // Carrega a categoria relacionada
         .ToListAsync();

        return result;

    }


    public async Task<Domain.Entities.Product> GetById(long productId) {  // essa é para atualizar
        return await _context.Products
            .Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.Id == productId);
    }


    public async Task<Domain.Entities.Product> GetProductById(long productId) {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(product => product.Id == productId);
    }

    public void Update(Domain.Entities.Product product) {
        _context.Products.Update(product);
    }
}
