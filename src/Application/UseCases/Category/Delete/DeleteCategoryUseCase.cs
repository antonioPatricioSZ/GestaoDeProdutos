using Domain.Repositories;
using Domain.Repositories.Category;
using Exceptions;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Category.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase {

    private readonly ICategoryWriteOnlyRepository _repositoryWrite;
    private readonly ICategoryReadOnlyRepository _repositoryRead;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryUseCase (
        ICategoryWriteOnlyRepository repositoryWrite, 
        ICategoryReadOnlyRepository repositoryRead,
        IUnitOfWork unitOfWork
    ){
        _repositoryWrite = repositoryWrite;
        _repositoryRead = repositoryRead;
        _unitOfWork = unitOfWork;
    }

    public async Task Executar(long categoryId) {

        var category = await _repositoryRead.GetCategoryById(categoryId);
        Validate(category);

        await _repositoryWrite.Delete(categoryId);

        await _unitOfWork.Commit();
        
    }

    private static void Validate(Domain.Entities.Category category) {

        if(category is null) {
            var mensagensDeErro = new List<string> { 
                ResourceErrorMessages.CATEGORIA_NAO_ENCONTRADA
            };
            throw new ValidationErrorsException(mensagensDeErro);
        }

    }

}
