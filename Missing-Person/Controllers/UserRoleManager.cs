using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;
using Missing_Person.Repository;
using Missing_Person.ViewModel;

namespace Missing_Person.Controllers
{
    //role admin only
    [Authorize(Roles = "Admin")]
    public class UserRoleManager : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMissingPersonRepository imissingPersonRepository;
        public UserRoleManager(RoleManager<IdentityRole> roleManager,
                       UserManager<User> userManager, IMissingPersonRepository imissingPersonRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.imissingPersonRepository = imissingPersonRepository;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public ViewResult DisplayAll()
        {
            var model = imissingPersonRepository.GetMissingPeopleAdmin();
            var displayAllViewModel = new DisplayAllViewModel
            {
                MissingPeople = model
            };
            return View(displayAllViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id) //Id
        {
            ViewBag.Id = Id;
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id.ToString(),
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditUsersInRole", new { Id = Id });
                }
            }

            return RedirectToAction("ListRole");
        }
        [HttpGet]
        public IActionResult Statistic()
        {
            return View();
        }


        public IActionResult Approve(int id)
        {
            var missingPerson = imissingPersonRepository.GetMissingPerson(id);
            missingPerson.IsApproved = true;
            MissingPerson updatedMissingPerson = imissingPersonRepository.UpdateMissingPerson(missingPerson);
            return RedirectToAction("DisplayAll");
        }
        public IActionResult Disapprove(int id)
        {
            var missingPerson = imissingPersonRepository.GetMissingPerson(id);
            missingPerson.IsApproved = false;
            MissingPerson updatedMissingPerson = imissingPersonRepository.UpdateMissingPerson(missingPerson);
            return RedirectToAction("DisplayAll");
        }
    }
}
