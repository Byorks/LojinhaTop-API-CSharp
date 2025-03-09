namespace LojinhaAPI.Requests;

public class UpdateUserRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public long typeUserId { get; set; }
}
