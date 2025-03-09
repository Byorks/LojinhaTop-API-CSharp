using LojinhaAPI.Domains;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using LojinhaAPI.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LojinhaAPI.Controllers;

/// <summary>
/// TypeUser Controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TypeUsersController : ControllerBase
{
    private readonly ITypeUserRepository typeUserRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public TypeUsersController(ITypeUserRepository typeUserRepository)
    {
        this.typeUserRepository = typeUserRepository;
    }

    /// <summary>
    /// List all TypeUsers
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ListAllTypeUsers(CancellationToken cancellationToken)
    {
        List<TypeUser> typeUsers = await typeUserRepository.ListAllAsync(cancellationToken);

        //List<TypeUserViewModel> typeUsersView = typeUsers.Select( x => new TypeUserViewModel(x.Id, x.Name));

        List<TypeUserViewModel> typeUsersView = new List<TypeUserViewModel>();

        foreach (TypeUser typeUser in typeUsers)
        {
            TypeUserViewModel typeUserViewModel = new (typeUser.Id, typeUser.Name);


            typeUsersView.Add(typeUserViewModel);
        }
        return Ok(typeUsersView);
    }
}
