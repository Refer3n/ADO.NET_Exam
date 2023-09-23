using System.Linq.Expressions;

namespace HotelApp.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        IEnumerable<T> GetAll();

        T Get(int id);

        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}
