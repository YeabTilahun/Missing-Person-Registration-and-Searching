using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;
using Missing_Person.Repository;
using Missing_Person.ViewModel;

namespace Missing_Person.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository iuserRepository;
        private readonly UserManager<User> userManager;
        public UserController(IUserRepository iuserRepository, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.iuserRepository = iuserRepository;
        }
        [HttpGet]
        public IActionResult MyProfile(User user)
        {
            var userID = userManager.GetUserId(HttpContext.User);
            User model = iuserRepository.GetUserById(userID);
            var displayAllUser = new DisplayAllUser
            {
                user = model
            };
            return View(displayAllUser);
        }
        [HttpGet]
        public IActionResult Detail(User user)
        {
            var  model = iuserRepository.GetUserById(user.Id);
            if(model == null)
            {
                return View("NotFound");
            }
            var displayAllUser = new DisplayAllUser
            {
                user = model
            };
            return View(displayAllUser);
        }
        [HttpGet]
        public IActionResult DisplayAll()
        {
            var model = iuserRepository.GetAllUser();
            if (model == null)
            {
                return View("NotFound");
            }
            var displayAllUser = new DisplayAllUser
            {
                users = model
            };
            return View(displayAllUser);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = iuserRepository.GetUserById(id);
            if (model == null)
            {
                return View("NotFound");
            }
            var displayAllUser = new DisplayAllUser
            {
                user = model
            };
            return View(displayAllUser); 
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
           var model = iuserRepository.UpdateUser(user);
            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(MissingPerson missingPerson)
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyPosts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MyPosts(MissingPerson missingPerson)
        {
            return View();
        }

    }
}
