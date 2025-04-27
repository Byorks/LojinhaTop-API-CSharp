namespace LojinhaAPI.Generics;
    
public class Result<T>
 {
    public T ResultObject { get; set; }
    public Error? Error { get; set; }

    // Utilizando uma palavra reservada como parâmetro
    public Result(T @object)
    {
        ResultObject = @object;
    }

    public Result(Error error)
    {
        Error = error;
    }
}
