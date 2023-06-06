using System.Net;
using System.Text;
using System.Text.Json;
using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers;

public class RegistrationPageController : Controller
{
    private readonly ILogger<RegistrationPageController> _logger;

    public RegistrationPageController(ILogger<RegistrationPageController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> AuthorizationPage(string Login, string Birthday, string Phone, string Email,
        string Password, string confirmPassword)
    {
        var person = new PersonDTO();

        if (Password == confirmPassword)
        {
            person.RightId = 1;
            person.Login = Login;
            person.Birthday = Convert.ToDateTime(Birthday);
            person.Phone = Phone;
            person.Email = Email;
            person.Password = Password;

            var httpClient = new HttpClient();
            using StringContent jsonContent = new(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync("http://localhost:5228/People/Registration", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var p = new Person();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
                return View("~/Views/Home/AuthorizationPage.cshtml", p);
            }

            return View("~/Views/Home/RegistrationPage.cshtml", p);
        }

        return View("~/Views/Home/RegistrationPage.cshtml", person);
    }
}