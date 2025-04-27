using LojinhaAPI.Domains;
using LojinhaAPI.Generics;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using LojinhaAPI.Requests;
using LojinhaAPI.Services.Interfaces;
using LojinhaAPI.ViewModel;
using LojinhaAPI.ViewModels;
using System.Net;

namespace LojinhaAPI.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        private readonly ITypeUserRepository typeUserRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserServices(IUserRepository userRepository, ITypeUserRepository typeUserRepository)
        {
            this.userRepository = userRepository;
            this.typeUserRepository = typeUserRepository;
        }

        public Task<IdViewModel> Create(UserRequest user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task Delete(long Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UserViewModel>> GetById(long Id, CancellationToken cancellationToken)
        {
            User? user = await userRepository
                .GetByIdAsync(Id, cancellationToken);

            if (user == null)
            {
                Error error = new Error(HttpStatusCode.NotFound, "Usuário não encontrado");
                return new Result<UserViewModel>(error);
            }
                

            TypeUserViewModel typeUserViewModel = new(user.TypeUserId, user.TypeUser.Name);
            UserViewModel userViewModel = new(user.Id, user.Name, user.Email, typeUserViewModel);
            
            return new Result<UserViewModel>(userViewModel);
        }
        public Task<Result<List<UserViewModel>>> ListAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserRequest user, long Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
