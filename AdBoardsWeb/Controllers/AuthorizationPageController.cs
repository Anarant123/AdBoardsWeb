using AdBoardsWeb.Models.db;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AdBoardsWeb.Controllers
{
	public class AuthorizationPageController : Controller
	{
		private readonly ILogger<AuthorizationPageController> _logger;

		public AuthorizationPageController(ILogger<AuthorizationPageController> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> Authorization(string login, string password)
		{
			var httpClient = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/People/Authorization?login={login}&password={password}");
			var response = await httpClient.SendAsync(request);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				Person user = new Person();
				user = JsonSerializer.Deserialize<Person>(responseContent)!;

				Context.UserNow = user;
				
				return View("~/Views/Home/ProfilePage.cshtml", user);
			}
			else
			{
				return View("~/Views/Home/AuthorizationPage.cshtml");
			}
		}

		public async Task<IActionResult> Recovery(string Login)
		{
			var httpClient = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5228/People/RecoveryPassword?Login={Login}");
			var response = await httpClient.SendAsync(request);
			var responseContent = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return View("~/Views/Home/AuthorizationPage.cshtml");
			}
			else
			{
				return View("~/Views/Home/AuthorizationPage.cshtml");
			}
		}
	}
}
