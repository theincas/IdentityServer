using IdentityServer.Areas.Admin.Models;
using IdentityServer.Extensions;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager = null)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name!
            }).ToListAsync();

            return View(roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole()
            {
                Name = request.Name
            });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }

            TempData["SuccessMessage"] = "Rol Başarıyla Oluşturuldu";
            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleUpdate = await _roleManager.FindByIdAsync(id);
            
            if (roleUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            }

            var role = new RoleUpdateViewModel()
            {
                Id = roleUpdate!.Id,
                Name = roleUpdate.Name!
            };

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roleUpdate = await _roleManager.FindByIdAsync(request.Id);

            if (roleUpdate == null)
            {
                throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            }

            roleUpdate.Name = request.Name;
            
            await _roleManager.UpdateAsync(roleUpdate);

            TempData["SuccessMessage"] = "Rol Bilgisi Güncellenmiştir.";

            return View();
        }

        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleDelete = await _roleManager.FindByIdAsync(id);

            if (roleDelete == null)
            {
                throw new Exception("Silinecek Rol Bulunamamıştır.");
            }

            var result = await _roleManager.DeleteAsync(roleDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x=>x.Description).First());
            }

            TempData["SuccessMessage"] = "Rol Başarıyla Silinmiştir";

            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = (await _userManager.FindByIdAsync(id))!;
            ViewBag.userId = id;

            var roles= await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var roleViewModel = new List<AssignRoleToUserViewModel>();

            foreach (var role in roles)
            {
                var assignRoleToUser = new AssignRoleToUserViewModel()
                {
                    Id = role.Id,
                    Name = role.Name!
                };

                //kullanıcının yetkileri roller arasından hangileri varsa exist true yapıyorum
                if (userRoles.Contains(role.Name!))
                {
                    assignRoleToUser.Exist = true;
                }

                roleViewModel.Add(assignRoleToUser);
            }

            return View(roleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId,List<AssignRoleToUserViewModel> requestList)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(currentUser!,role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(currentUser!,role.Name);
                }
            }

            return RedirectToAction(nameof(HomeController.UserList),"Home");
        }
    }
}
