using LojinhaAPI.Domains;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using LojinhaAPI.Requests;
using LojinhaAPI.ViewModel;
using LojinhaAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LojinhaAPI.Controllers;


/// <summary>
/// User Controllers
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly ITypeUserRepository typeUserRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    public UsersController(IUserRepository userRepository, ITypeUserRepository typeUserRepository)
    {
        this.userRepository = userRepository;
        this.typeUserRepository = typeUserRepository;
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Return an user by id</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        User? user = await userRepository
            .GetByIdAsync(id, cancellationToken);

        if (user == null)
            return NotFound("Usuário não encontrado.");

        TypeUserViewModel typeUserViewModel = new(user.TypeUserId, user.TypeUser.Name);
        UserViewModel userViewModel = new(user.Id, user.Name, user.Email, typeUserViewModel);

        return Ok(userViewModel);
    }

    /// <summary>
    /// Get All Users
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        List<User> users = await userRepository
            .ListAllAsync(cancellationToken);

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

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Return id of User created</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequest user, CancellationToken cancellationToken)
    {
        // Validação do usuário
        if (await userRepository.EmailExistsAsync(user.Email, cancellationToken))
            return BadRequest($"Usuário com email {user.Email} já existe no sistema");

        if (!await typeUserRepository.TypeUserExistsAsync(user.TypeUserId, cancellationToken))
            return BadRequest($"Tipo de usuário inválido.");

        User newUser = new User(user.Name, user.Email, user.TypeUserId);

        User userCreated = await userRepository.CreateAsync(newUser, cancellationToken);

        return Ok(new IdViewModel(userCreated.Id));
    }



    /// <summary>
    /// Delete user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Name of deleted user </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user == null)
            return BadRequest("Usuário não encontrado");

        await userRepository.DeleteAsync(user, cancellationToken);

        // Return desnecessário, sempre analisar se o que estamos retornando faz sentido
        return Ok("Usuário deletado");
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated User</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest user, CancellationToken cancellationToken)
    {

        // Feedback
        // Diogo mencionou o fato de uma repetição de código estar acontecendo na userRepository
        // Outro ponto é a consulta ao banco repetidas vezes
        // Vamos resolver isso >:D


        // Em vez de eu verificar se existe o Id e depois pegar o usuário, já vamos tentar pegar ele com o método já criado
        User? userToBeUpdated = await userRepository.GetByIdAsync(user.Id, cancellationToken);


        // Precisa ver se todas as entradas do update sao validas
        // Id existe?
        if (userToBeUpdated == null)
            // Atenção ao retorno ao front, sempre tentar específicar o motivo do erro - nesse caso "Usuário não encontrado" encaixaria melhor
            return NotFound("Id inválido");

        // O email é valido?
        if( userToBeUpdated.Email != user.Email)
        { 
            if (await userRepository.EmailExistsAsync(user.Email, cancellationToken))
                return BadRequest($"Usuário com email {user.Email} já existe no sistema");
        }

        // O TypeUser é válido
        if (!await typeUserRepository.TypeUserExistsAsync(user.TypeUserId, cancellationToken))
            return NotFound("Tipo de úsuário não existe no sistema.");

        // Atualiza
        userToBeUpdated.Name = user.Name;
        userToBeUpdated.Email = user.Email;
        userToBeUpdated.TypeUserId = user.TypeUserId;

        // Se existe nós atualizamos com as informações do corpo
        User userUpdate = await userRepository.UpdateAsync(userToBeUpdated, cancellationToken);

        // Ver necessidade do uso do retorno
        UserViewModel updatedUser = new ( userUpdate.Id, userUpdate.Name, userUpdate.Email, new(userUpdate.TypeUser.Id, userUpdate.TypeUser.Name));

        // Returna ok se estiver dado certo
        return Ok(updatedUser);
    }


    #region Minhas Tentativas
    // Tentativa de criar endpoint que retorna usuario por id
    //[HttpGet("{id}")]
    //public IActionResult GetUserById(int id)
    //{
    //    List<User> users = userRepository.Users
    //        .Include(x => x.TypeUser)
    //        .ToList();

    //    var user = users.Find(x => x.Id == id);

    //    if (user != null)
    //    {
    //        UserViewModel userViewModel = new UserViewModel(user.Id, user.Name, user.Email, new TypeUserViewModel(user.TypeUserId, user.TypeUser.Name));
    //        return Ok(userViewModel);
    //    }
    //    else
    //    {
    //        return NotFound();
    //    }     
    //}
    #endregion

}
