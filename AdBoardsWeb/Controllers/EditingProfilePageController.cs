using Microsoft.AspNetCore.Mvc;

namespace AdBoardsWeb.Controllers
{
    public class EditingProfilePageController : Controller
    {
        private readonly ILogger<EditingProfilePageController> _logger;

        public EditingProfilePageController(ILogger<EditingProfilePageController> logger)
        {
            _logger = logger;
        }
        public IActionResult SetPhoto()
        {
            return View();
        }
    }
}
