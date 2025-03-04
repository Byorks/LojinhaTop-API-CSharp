namespace LojinhaAPI.ViewModels;

public class TypeUserViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }

    public TypeUserViewModel(long id, string name)
    {
        Id = id;
        Name = name;
    }
}
