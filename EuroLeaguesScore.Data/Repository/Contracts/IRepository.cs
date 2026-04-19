using System.Linq.Expressions;

namespace EuroLeaguesScore.Data.Repository.Contracts
{
    public interface IRepository<TType, TId>
    {
        Task<TType> GetByIdAsync(TId id);

        Task<TType> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);

        Task<IEnumerable<TType>> GetAllAsync();

        IQueryable<TType> GetAllAttached();

        Task AddAsync(TType item);

        Task AddRangeAsync(TType[] items);

        Task<bool> DeleteAsync(TType entity);

        Task<bool> UpdateAsync(TType item);

        Task<TType> FindByConditionsAsync(Expression<Func<TType, bool>> predicate);

        Task SaveChangesAsync();
    }
}
