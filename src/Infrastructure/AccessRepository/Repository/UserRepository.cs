using Domain.Entities;
using Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository.Repository;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository {

    private readonly GestaoDeProdutosContext _context;

    public UserRepository(GestaoDeProdutosContext context) {
        _context = context;
    }


    public async Task Register(User user) {
        await _context.Users.AddAsync(user);
    }


    public async Task<User> Login(string email, string password) {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                user => user.Email.Equals(email) && user.Password.Equals(password)
            );
    }


    public async Task<bool> UserEmailExists(string email) {
        return await _context.Users.AnyAsync(user => user.Email.Equals(email));
    }
}
