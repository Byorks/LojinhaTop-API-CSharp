using LojinhaAPI.Generics;
using LojinhaAPI.Requests;
using LojinhaAPI.ViewModel;
using LojinhaAPI.ViewModels;

namespace LojinhaAPI.Services.Interfaces
{
    public interface IUserServices
    {
        Task<Result<List<UserViewModel>>> ListAll(CancellationToken cancellationToken);
        Task<Result<UserViewModel>> GetById(long Id, CancellationToken cancellationToken);
        Task<IdViewModel> Create(UserRequest user, CancellationToken cancellationToken);
        Task Update(UserRequest user, long Id, CancellationToken cancellationToken);
        Task Delete(long Id, CancellationToken cancellationToken);
    }
}
