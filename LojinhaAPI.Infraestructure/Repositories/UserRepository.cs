using LojinhaAPI.Domains;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojinhaAPI.Infraestructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LojinhaDbContext db;

    public UserRepository(LojinhaDbContext db)
    {
        this.db = db;
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var newUser = await db.Users
            .AddAsync(user, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);

        return newUser.Entity;
    }

    public void DeleteAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        // ? Permite a classe ser nullable
        User? user = await db.Users
             .Include(x => x.TypeUser)
             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return user;
    }

    public async Task<List<User>> ListAllAsync(CancellationToken cancellationToken)
    {
        List<User> users = await db.Users
            .Include(x => x.TypeUser)
            .ToListAsync(cancellationToken);

        return users;
    }

    public Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    => await db.Users.AnyAsync(x => x.Email.Contains(email), cancellationToken);    
}

