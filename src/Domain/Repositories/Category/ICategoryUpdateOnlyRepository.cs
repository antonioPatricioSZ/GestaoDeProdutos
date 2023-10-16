namespace Domain.Repositories.Category;

public interface ICategoryUpdateOnlyRepository {

    Task<Entities.Category> GetById(long categoryId);

    void Update(Entities.Category category);
}
