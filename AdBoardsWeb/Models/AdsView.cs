using AdBoards.ApiClient.Contracts.Responses;

namespace AdBoardsWeb.Models;

public class AdsView
{
    public FilterModel Filter { get; set; } = new();

    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
}