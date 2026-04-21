namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.ViewModels.Player;
    using EuroLeaguesScore.ViewModels.Team;

    public interface ITeamService
    {
        Task<IEnumerable<AllTeamViewModel>> AllTeamsOrderedByLeagueNameThenByNameAsync(string? userId);

        Task<IEnumerable<ManagerViewModel>> GetManagersWhichHaveNotTeamToManageAsync();

        Task<IEnumerable<ManagerViewModel>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId);

        Task<IEnumerable<LeagueViewModel>> GetLeaguesOrderedByLeagueNameAsync();

        Task AddTeamToDbAsync(AddTeamInputModel model);

        Task<IEnumerable<DetailsPlayerViewModel>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId);

        Task<DetailsTeamInputModel?> GetDetailsTeamViewModelAsync(int teamId, string userId);

        Task<EditTeamInputModel?> GetEditTeamViewModelAsync(int teamId);

        Task<bool> EditTeamToDbAsync(int teamId, EditTeamInputModel model);

        Task<DeleteTeamViewModel?> GetDeleteTeamViewModelAsync(int teamId);

        Task<bool> DeleteTeamFromDbAsync(int teamId, DeleteTeamViewModel model);
    }
}
