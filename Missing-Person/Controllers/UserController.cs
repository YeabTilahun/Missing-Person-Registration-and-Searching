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
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            EditUserViewModel model = new EditUserViewModel
            {
               users = user,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User));

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.users.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.FirstName = model.users.FirstName;
                user.LastName = model.users.LastName;
                user.UserName = model.users.Email;
                user.Password = model.users.Password;
                user.City = model.users.City;
                user.Country = model.users.Country;
                user.Address = model.users.Address;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyProfile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("DisplayAll");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("DisplayAll");
            }       
        }
       
    }
}
