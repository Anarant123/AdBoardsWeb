using newAds = AdBoards.ApiClient.Contracts.Responses;

namespace AdBoardsWeb.Models.db;

public class AdListViewModel
{
    public List<newAds.Ad>? Ads { get; set; }
}