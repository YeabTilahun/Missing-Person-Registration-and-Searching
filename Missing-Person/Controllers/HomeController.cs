using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;
using Missing_Person.Repository;
using Missing_Person.ViewModel;
using System.Diagnostics;

namespace Missing_Person.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMissingPersonRepository imissingPersonRepository;
        private readonly UserManager<User> userManager;
        public HomeController(IMissingPersonRepository imissingPersonRepository, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.imissingPersonRepository = imissingPersonRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DisplayAll");
        }
        [AllowAnonymous]
        [HttpGet]
        public ViewResult DisplayAll()
        {
            var model = imissingPersonRepository.GetMissingPeople();
            var displayAllViewModel = new DisplayAllViewModel
            {
                MissingPeople = model
            };
            return View("Index",displayAllViewModel);
        }
        public IActionResult Privacy()
        {
            return View("NotFound");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}