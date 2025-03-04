using LojinhaAPI.Models;
using LojinhaAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojinhaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly LojinhaDbContext db;

    public UsersController(LojinhaDbContext db)
    {
        // Injecao de dependencia
        this.db = db;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        List<User> users = db.Users
            .Include(x => x.TypeUser)
            .ToList();
        
        // Criando uma lista de UserViewModel
        List<UserViewModel> usersViewModels = new List<UserViewModel>();

        // Forma "na mão"
        foreach (var user in users)
        {
            TypeUserViewModel typeUserViewModel = new(user.TypeUserId, user.TypeUser.Name);
            UserViewModel userViewModel = new(user.Id, user.Name, user.Email, typeUserViewModel);

            usersViewModels.Add(userViewModel);
        }

        // Forma com LINQ (mais enxuta)
        //var usersViewModels =  users.Select(x => new UserViewModel(x.Id,x.Name,x.Email,
        //                                         new TypeUserViewModel(x.TypeUserId, x.TypeUser.Name)))
        //                                        .ToList();

        
        // Retornando a lista de usuários
        return Ok(usersViewModels); // 200 OK
    }
}
