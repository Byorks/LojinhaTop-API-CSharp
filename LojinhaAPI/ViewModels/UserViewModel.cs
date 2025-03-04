namespace LojinhaAPI.ViewModels;

public class UserViewModel
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public TypeUserViewModel TypeUser { get; set; }
    
    //ctor - atalho
    public UserViewModel(long id, string userName, string email, TypeUserViewModel typeUser)
    {
        Id = id;
        UserName = userName;
        Email = email;
        TypeUser = typeUser;
    }
}
