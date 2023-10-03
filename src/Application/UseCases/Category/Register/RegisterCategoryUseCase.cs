using AutoMapper;
using Communication.Requests;
using Communication.Responses;
using Domain.Repositories;
using Domain.Repositories.Category;
using Exceptions.ExceptionsBase;

namespace Application.UseCases.Category.Register;

public class RegisterCategoryUseCase : IRegisterCategoryUseCase {

    private readonly IMapper _mapper;
    private readonly ICategoryWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCategoryUseCase(
        IMapper mapper,
        ICategoryWriteOnlyRepository repository,
        IUnitOfWork unitOfWork
    ){
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseCategoryJson> Executar(RequestRegisterCategoryJson request) {
        Validate(request);

        var categoria = _mapper.Map<Domain.Entities.Category>(request);

        await _repository.RegisterCategory(categoria);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseCategoryJson>(categoria);

    }


    private static void Validate(RequestRegisterCategoryJson request) {
        var validator = new RegisterCategoryValidator();
        var resultado = validator.Validate(request);

        if(!resultado.IsValid) {
            var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ValidationErrorsException(mensagensDeErro);
        }
    }

}
