using IdentityServer.Areas.Admin.Models;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userList= await _userManager.Users.ToListAsync();
            var userViewModelList=userList.Select(x=> new UserViewModel
            {
                UserId = x.Id,
                Email=x.Email!,
                UserName=x.UserName!
            }).ToList();
            return View(userViewModelList);
        }
    }
}
