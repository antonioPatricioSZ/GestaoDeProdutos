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
    }


    private void EntityToResponse() {
        
    }

}
