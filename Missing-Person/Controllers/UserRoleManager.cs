using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Person.Models;
using Missing_Person.ViewModel;

namespace Missing_Person.Controllers
{
    public class UserRoleManager : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public UserRoleManager(RoleManager<IdentityRole> roleManager,
                       UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
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
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        /*   public async Task<IActionResult> EditUserRole(string id)
           {
               ViewBag.userId = id;
               var user = await userManager.FindByIdAsync(id);
               var role = await roleManager.FindByIdAsync(id);

               if(user == null)
               {
                   ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                   return View("NotFound");
               }
               var model = new List<EditUserRoleViewModel>();
               foreach(var users in userManager.Users)
               {
                   var userRoleViewModel = new EditUserRoleViewModel
                   {
                       UserId = users.Id.ToString(),
                       UserName  = users.UserName
                   };
                   if(await userManager.IsInRoleAsync(user, users.UserName))
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
           }*/

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId) //Id
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            //userManager.Users.ToList()
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
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
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
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
