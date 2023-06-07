namespace AdBoardsWeb.Models;

public class FilterModel
{
    public int Type { get; set; }

    public string? PriceFrom { get; set; }

    public string? PriceUpTo { get; set; }

    public string? City { get; set; }

    public int CategoryId { get; set; }

    public int AdTypeId { get; set; }
}