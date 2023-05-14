using AdBoardsWeb.DTO;
using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AdBoardsWeb.Controllers
{
    public class EditingProfilePageController : Controller
    {
        private readonly ILogger<EditingProfilePageController> _logger;
        PersonDTO person = new PersonDTO();
        public EditingProfilePageController(ILogger<EditingProfilePageController> logger)
        {
            _logger = logger;
        }
        public IActionResult SetPhoto(IFormFile file)
        {
            person.Login = Context.UserNow.Login;
            person.Name = Context.UserNow.Name;
            person.Phone = Context.UserNow.Phone;
            person.Email = Context.UserNow.Email;
            person.Birthday = Context.UserNow.Birthday;
            person.City = Context.UserNow.City;

            person.Photo = System.IO.File.ReadAllBytes(file.FileName);


            return View("~/Views/Home/EditingProfilePage.cshtml", person);
        }

        public async Task<IActionResult> SaveProfileChanges(string Name, string Phone, string Email, string Birthday, string City)
        {
            person.Login = Context.UserNow.Login;
            person.Name = Name;
            person.Phone = Phone;
            person.Email = Email;
            person.Birthday = Convert.ToDateTime(Birthday);
            person.City = City;

            var httpClient = new HttpClient();
            using StringContent jsonContent = new ( JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("http://localhost:5228/People/Update", jsonContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Person p = JsonSerializer.Deserialize<Person>(jsonResponse)!;
                Context.UserNow = p;
                return View("~/Views/Home/ProfilePage.cshtml", p);
            }
            else
            {
                return View("~/Views/Home/EditingProfilePage.cshtml", person);
            }
        }

        public IActionResult EditingProfilePage()
        {
            return View("~/Views/Home/EditingProfilePage.cshtml", Context.UserNow);
        }
    }
}
