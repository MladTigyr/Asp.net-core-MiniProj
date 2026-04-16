namespace EuroLeaguesScore.GCommon
{
    public static class EntityValidations
    {
        //TeamValidations
        public const int TeamNameMinLength = 3;
        public const int TeamNameMaxLength = 50;
    
        public const int TeamCountryMinLength = 4;
        public const int TeamCountryMaxLength = 56;
    
        public const int TeamCityMinLength = 2;
        public const int TeamCityMaxLength = 150;

        //LeagueValidations
        public const int LeagueNameMinLength = 3;
        public const int LeagueNameMaxLength = 50;

        //PlayerValidations
        public const int PlayerNameMinLength = 3;
        public const int PlayerNameMaxLength = 100;
        public const int PlayerAgeMin = 15;
        public const int PlayerAgeMax = 55;

        //ManagerValidations
        public const int ManagerNameMinLength = 3;
        public const int ManagerNameMaxLength = 100;

        //Wins/Losses/Draws/Goals/Assists Validations
        public const int MinWinsLossesDraws = 0;
        public const int MaxWinsLossesDraws = int.MaxValue;
    }
}
