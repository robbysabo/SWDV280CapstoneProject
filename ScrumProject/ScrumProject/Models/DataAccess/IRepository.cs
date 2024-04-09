namespace ScrumProject.Models.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);
        T Get(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
