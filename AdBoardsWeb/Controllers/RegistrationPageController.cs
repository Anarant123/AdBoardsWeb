using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace AdBoardsWeb.Controllers
{
    public class RegistrationPageController : Controller
    {
        private readonly ILogger<RegistrationPageController> _logger;

        public RegistrationPageController(ILogger<RegistrationPageController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> AuthorizationPage(string Login, string Birthday, string Phone, string Email, string Password, string confirmPassword)
        {
            PersonDTO person = new PersonDTO();

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
                using HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5228/People/Registration", jsonContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                Person p = new Person();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
                    return View("~/Views/Home/AuthorizationPage.cshtml", p);
                }
                else
                {
                    return View("~/Views/Home/RegistrationPage.cshtml", p);
                }
            }
            else
            {
                return View("~/Views/Home/RegistrationPage.cshtml", person);
            }
        }
    }
}
