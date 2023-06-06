using System.Net;
using System.Text;
using System.Text.Json;
using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

public class EditingProfilePageController : Controller
{
    private readonly ILogger<EditingProfilePageController> _logger;
    private readonly PersonDTO person = new();

    public EditingProfilePageController(ILogger<EditingProfilePageController> logger)
    {
        _logger = logger;
    }

    public IActionResult SetPhoto(string filephoto, string Name, string Phone, string Email, string Birthday,
        string City)
    {
        person.Name = Name;
        person.Phone = Phone;
        person.Email = Email;
        person.Birthday = Convert.ToDateTime(Birthday);
        person.City = City;
        person.Photo = System.IO.File.ReadAllBytes(filephoto);
        ViewBag.Photo = filephoto;

        return View("~/Views/Home/EditingProfilePage.cshtml", person);
    }

    public async Task<IActionResult> SaveProfileChanges(string filephoto, string Name, string Phone, string Email,
        string Birthday, string City)
    {
        /*person.Login = Context.UserNow.Login;
        person.Name = Name;
        person.Phone = Phone;
        person.Email = Email;
        person.Birthday = Convert.ToDateTime(Birthday);
        person.City = City;
        if (!string.IsNullOrEmpty(filephoto))
            person.Photo = System.IO.File.ReadAllBytes(filephoto);


        var httpClient = new HttpClient();
        using StringContent jsonContent = new(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
        using var response = await httpClient.PutAsync("http://localhost:5228/People/Update", jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
            Context.UserNow = p;
            return View("~/Views/Home/ProfilePage.cshtml", p);
        }

        return View("~/Views/Home/EditingProfilePage.cshtml", person);*/
        throw new Exception();
    }

    public IActionResult EditingProfilePage()
    {
        /*return View("~/Views/Home/EditingProfilePage.cshtml", Context.UserNow);*/
        throw new Exception();
    }
}