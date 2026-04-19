namespace EuroLeaguesScore.Data.Repository.Contracts
{
    using EuroLeaguesScore.Data.Models;

    public interface IManagerRepository : IRepository<Manager, int>
    {
        Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageAsync();

        Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId);
    }
}
