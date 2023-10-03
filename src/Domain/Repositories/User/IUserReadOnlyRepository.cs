namespace Domain.Repositories.User;

public interface IUserReadOnlyRepository {

    Task<bool> UserEmailExists(string email);

    Task<Entities.User> Login(string email, string password);

    Task<Entities.User> RecoverById(long userId);

}
