using AdBoards.ApiClient.Contracts.Responses;
using AdBoardsWeb.Models;

namespace AdBoardsWeb.ViewModels;

public class AdsViewModel
{
    public FilterModel Filter { get; set; } = new();

    public ICollection<Ad> Ads { get; set; } = new List<Ad>();
}