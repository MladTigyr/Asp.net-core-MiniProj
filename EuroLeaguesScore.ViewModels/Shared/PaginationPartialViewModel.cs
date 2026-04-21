public class PaginationPartialViewModel
{
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public string Action { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public string? SearchTerm { get; set; }

    public int? LeagueId { get; set; }
}