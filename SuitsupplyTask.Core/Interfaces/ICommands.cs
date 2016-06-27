namespace SuitsupplyTask.Core.Interfaces
{
    public interface ICommands<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
