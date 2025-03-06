using LojinhaAPI.Domains;

namespace LojinhaAPI.Infraestructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> ListAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<User?> DeleteAsync(long id, CancellationToken cancellationToken);

    // Nomes de funcoes precisam fazer sentido
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
}
