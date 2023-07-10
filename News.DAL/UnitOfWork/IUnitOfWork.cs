namespace TahliliTask.DAL;

public interface IUnitOfWork
{
    public IAuthorsRepo AuthorsRepo { get; }
    public INewsRepo NewsRepo { get; }
    int Save();
}
