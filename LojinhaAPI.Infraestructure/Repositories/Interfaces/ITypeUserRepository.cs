using LojinhaAPI.Domains;

namespace LojinhaAPI.Infraestructure.Repositories.Interfaces;

public interface ITypeUserRepository
{
    Task<List<TypeUser>> ListAllAsync(CancellationToken cancellationToken);
    Task<bool> TypeUserExistsAsync(long id, CancellationToken cancellationToken);
}
