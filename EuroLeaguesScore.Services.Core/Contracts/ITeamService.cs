namespace EuroLeaguesScore.Services.Core.Contracts
{
    using EuroLeaguesScore.Data.Models;
    using EuroLeaguesScore.ViewModels.Player;
    using EuroLeaguesScore.ViewModels.Team;

    public interface ITeamService
    {
        public Task<IEnumerable<AllTeamViewModel>> AllTeamsOrderedByLeagueNameThenByNameAsync(string? userId);

        public Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageAsync();

        public Task<IEnumerable<Manager>> GetManagersWhichHaveNotTeamToManageOrCurrentTeamManagerAsync(int teamId);

        public Task<IEnumerable<League>> GetLeaguesOrderedByLeagueNameAsync();

        public Task AddTeamToDbAsync(AddTeamInputModel model);

        public Task<IEnumerable<DetailsPlayerViewModel>> GetDetailsPlayersOrderedByNameWithTeamIdAsync(int teamId);

        public Task<Team?> GetTeamByIdAsync(int teamId);
        public Task<Team?> GetTeamByIdTrackingAsync(int teamId);
        public Task<Team?> GetTeamByIdTrackingWithoutIncludingAsync(int teamId);

        public Task<DetailsTeamInputModel?> GetDetailsTeamViewModelAsync(int teamId, string userId);

        public Task<EditTeamInputModel?> GetEditTeamViewModelAsync(int teamId);

        public Task<bool> EditTeamToDbAsync(int teamId, EditTeamInputModel model);

        public Task<DeleteTeamViewModel?> GetDeleteTeamViewModelAsync(int teamId);

        public Task<bool> DeleteTeamFromDbAsync(int teamId, DeleteTeamViewModel model);
    }
}
