
namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILeagueRepository : IRepository<League, int>
    {
        Task<IEnumerable<League>> GetLeaguesOrderedByLeagueNameAsync();
    }
}
