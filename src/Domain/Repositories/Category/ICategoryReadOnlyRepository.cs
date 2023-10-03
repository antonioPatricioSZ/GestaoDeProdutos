namespace Domain.Repositories.Category;

public interface ICategoryReadOnlyRepository {

    Task<IList<Entities.Category>> GetAllCategories();

    Task<Entities.Category> GetCategoryById(long categoryId);

}
