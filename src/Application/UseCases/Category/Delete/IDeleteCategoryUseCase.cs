namespace Application.UseCases.Category.Delete;

public interface IDeleteCategoryUseCase {

    Task Executar(long categoryId);

}
