namespace Domain.Repositories.Category;


public interface ICategoryWriteOnlyRepository {

    Task RegisterCategory(Entities.Category category);

    //Task Delete(long categoryId);

}
