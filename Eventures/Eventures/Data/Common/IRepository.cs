using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Data.Common
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
