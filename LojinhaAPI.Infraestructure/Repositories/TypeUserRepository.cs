using LojinhaAPI.Domains;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojinhaAPI.Infraestructure.Repositories;

public class TypeUserRepository : ITypeUserRepository
{
    private readonly LojinhaDbContext db;

    public TypeUserRepository(LojinhaDbContext db)
    {
        this.db = db;
    }

    // Exercicio : Implementar os metodos abaixo
    public Task<List<TypeUser>> ListAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> TypeUserExistsAsync(long id, CancellationToken cancellationToken)
    => await db.Users.AnyAsync(x => x.Id == id, cancellationToken);
}
