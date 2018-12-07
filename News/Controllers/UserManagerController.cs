using News.DTO;
using News.Interface;
using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly IUsersRepository _usersRepo;
        private readonly IRolesRepository _rolesRepo;

        public UserManagerController(IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _usersRepo = usersRepository;
            _rolesRepo = rolesRepository;
        }
        // GET: UserManager
        public async Task<ActionResult> Index()
        {
            var get = await _usersRepo.GetAllAsync();
            var model = new List<UserViewModel>();

            foreach (var item in get)
            {
                var temp = new UserViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    FullName = item.FullName,
                    PhoneNumber = item.PhoneNumber,
                    RoleId = item.RoleId,
                    RoleName = item.RoleName
                };

                model.Add(temp);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var getlistRole = await _rolesRepo.GetAllAsync();
            ViewBag.listRoles = getlistRole;

            var get = await _usersRepo.FindByIdAsync(id);

            var model = new UserViewModel
            {
                Id = get.Id,
                UserName = get.UserName,
                Email = get.Email,
                FullName = get.FullName,
                PhoneNumber = get.PhoneNumber,
                RoleId = get.RoleId,
                RoleName = get.RoleName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel vm)
        {
            var deleteRole = await _rolesRepo.DeleteUserRolesAsync(vm.Id);

            var userRole = new UserRoles
            {
                UserId = vm.Id,
                RoleId = vm.RoleId
            };

            var createRole = await _rolesRepo.CreateUserRolesAsync(userRole);

            var user = new Users()
            {
                Id = vm.Id,
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.UserName,
                PhoneNumber = vm.PhoneNumber
            };

            var updateUser = await _usersRepo.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _usersRepo.DeleteAsync(id);

            if (delete)
            {
                var mess = new
                {
                    Success = delete
                };
                return Json(mess, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var mess = new
                {
                    isSuccess = delete
                };
                return Json(mess, JsonRequestBehavior.AllowGet);
            }
        }
    }
}