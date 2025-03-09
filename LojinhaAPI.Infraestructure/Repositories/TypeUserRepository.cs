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
    public async Task<List<TypeUser>> ListAllAsync(CancellationToken cancellationToken)
    {
        List<TypeUser> typeUsers = await db.TypeUsers.ToListAsync(cancellationToken);

        return (typeUsers);
    }

    public async Task<bool> TypeUserExistsAsync(long id, CancellationToken cancellationToken)
    => await db.Users.AnyAsync(x => x.TypeUserId == id, cancellationToken);
}
