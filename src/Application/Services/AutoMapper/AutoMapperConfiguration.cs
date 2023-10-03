using AutoMapper;

namespace Application.Services.AutoMapper;

public class AutoMapperConfiguration : Profile {

    public AutoMapperConfiguration() {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity() {
        CreateMap<Communication.Requests.JsonUserRegistrationRequest, Domain.Entities.User>()
            .ForMember(destino => destino.Password, config => config.Ignore());

        CreateMap<Communication.Requests.RequestProductJson, Domain.Entities.Product>();

        CreateMap<Communication.Requests.RequestRegisterCategoryJson, Domain.Entities.Category>();
    }


    private void EntityToResponse() {
        CreateMap<Domain.Entities.Product, Communication.Responses.ResponseProductJson>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

        CreateMap<Domain.Entities.Product, Communication.Responses.ResponseGetProductJson>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        
        CreateMap<Domain.Entities.Category, Communication.Responses.ResponseCategoryJson>();
    }
    
}
