using System.Net;
using System.Text;
using System.Text.Json;
using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

public class MyAdPageController : Controller
{
    private readonly ILogger<MyAdPageController> _logger;
    private readonly AdDTO ad = new();

    public MyAdPageController(ILogger<MyAdPageController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> SaveChanges(string Name, string Description, string Price, string City,
        string Cotegory)
    {
        ad.Id = Context.AdNow.Id;
        ad.Name = Name;
        ad.Description = Description;
        ad.CotegorysId = Convert.ToInt32(Cotegory);
        ad.Price = Convert.ToInt32(Price);
        ad.City = City;
        ad.Photo = Context.AdNow.Photo;


        var httpClient = new HttpClient();
        using StringContent jsonContent = new(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json");
        using var response = await httpClient.PutAsync("http://localhost:5228/Ads/Update", jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();


        /*if (response.StatusCode == HttpStatusCode.OK)
        {
            var a = JsonSerializer.Deserialize<Ad>(jsonResponse)!;
            a.Person = Context.UserNow;
            Context.AdNow = a;
            return View("~/Views/Home/MyAdPage.cshtml", a);
        }

        var adNow = new Ad();
        adNow.Id = Context.AdNow.Id;
        adNow.Name = Name;
        adNow.CotegorysId = Convert.ToInt32(Cotegory);
        adNow.Description = Description;
        adNow.Price = Convert.ToInt32(Price);
        adNow.City = City;
        adNow.Person = Context.UserNow;
        adNow.Photo = Context.AdNow.Photo;
        return View("~/Views/Home/MyAdPage.cshtml", adNow);*/
        throw new Exception();
    }

    public async Task<IActionResult> Delete()
    {
        /*var httpClient = new HttpClient();
        using var responseD = await httpClient.DeleteAsync($"http://localhost:5228/Ads/Delete?id={Context.AdNow.Id}");
        using var response = await httpClient.GetAsync($"http://localhost:5228/Ads/GetMyAds?id={Context.UserNow.Id}");
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Context.AdList = new AdListViewModel();

            Context.AdList.Ads = JsonSerializer.Deserialize<List<Ad>>(responseContent);
            ViewBag.IsSuccess = true;
            return View("~/Views/Home/MyAdsPage.cshtml", Context.AdList);
        }

        ViewBag.IsSuccess = false;*/
        return View("~/Views/Home/MyAdsPage.cshtml");
    }
}