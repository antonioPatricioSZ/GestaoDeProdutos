using Domain.Entities;
using Domain.Repositories.Category;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository.Repository;

public class CategoryRepository : 
    ICategoryWriteOnlyRepository, 
    ICategoryReadOnlyRepository,
    ICategoryUpdateOnlyRepository {

    private readonly GestaoDeProdutosContext _context;

    public CategoryRepository(GestaoDeProdutosContext context) {
        _context = context;
    }

    public async Task RegisterCategory(Category category) {
        await _context.Categories.AddAsync(category);
    }


    public async Task<IList<Category>> GetAllCategories() {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }


    public async Task<Category> GetCategoryById(long categoryId) {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == categoryId);
    }

    public async Task Delete(long categoryId) {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        _context.Categories.Remove(category);
    }

    public async Task<Category> GetById(long categoryId) { // essa é para atualizar
        return await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == categoryId);
    }

    public void Update(Category category) {
        _context.Categories.Update(category);
    }
}
