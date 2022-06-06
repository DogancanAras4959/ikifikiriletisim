using AutoMapper;
using ikifikir.COMMON.DataTransfer.RoleData;
using ikifikir.COMMON.DataTransfer.UserData;
using ikifikir.CORE.Helper;
using ikifikir.CORE.Helper.Cyrptography;
using ikifikir.CORE.Helper.Extends;
using ikifikir.editor.Models.RolModel;
using ikifikir.editor.Models.UserModel;
using ikifikir.ENGINES.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ikifikir.editor.Controllers
{

    public class kullaniciController : Controller
    {

        #region Constructure

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public kullaniciController(IUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        #endregion

        #region Login

        [HttpGet("girisyap")]
        public IActionResult girisyap(string message = "")
        {
            if (message == null)
            {
                ViewBag.LTD = Request.Cookies["LastLoggedInTime"];
                return View();
            }

            ViewBag.LTD = Request.Cookies["LastLoggedInTime"];
            ViewBag.Message = message;
            return View(new LoginViewModel());
        }

        [HttpGet("[controller]/girisyap")]
        public IActionResult NavigateLogin(string returnUrl)
        {
            return RedirectToAction("girisyap", "kullanici", new { ReturnUrl = returnUrl });
        }

        [HttpPost("girisyap")]
        public async Task<IActionResult> girisyap(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                string passCrypto = new Cyrpto().TryEncrypt(model.Password);
                bool result = await _userService.LoginAsync(model.Email, passCrypto);

                if (result == false)
                {
                    TempData["HataMesaji"] = "Kullanıcının giriş işlemi başarısız oldu!";
                    return View("girisyap");
                }

                else
                {
                    UserViewModel getUser = _mapper.Map<UserDto, UserViewModel>(_userService.GetUserByName(model.Email));

                    if (getUser.IsActive == true)
                    {

                        Response.Cookies.Append("LastLoggedInTime", DateTime.Now.ToString());

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, getUser.UserName),
                            new Claim(ClaimTypes.NameIdentifier, getUser.Id.ToString()),
                        };

                        var claims_identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var claimsPrincipal = new ClaimsPrincipal(claims_identity);

                        var auth_properties = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)
                        };

                        await HttpContext.SignInAsync(claimsPrincipal, auth_properties);

                        return RedirectToAction("anasayfa", "yonetim");
                    }
                    else
                    {
                        TempData["HataMesaji"] = "Kullanıcı aktif edilmemiş";
                        return View("girisyap");
                    }
                }

            }
            else
            {
                TempData["HataMesaji"] = "Kullanıcı adı ve şifrenizi girmelisiniz";
                return View("girisyap");
            }
        }

        [HttpGet("yetkisizgiris")]
        public IActionResult yetkisizgiris()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> cikisyap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region Login Eski

        //[HttpPost]
        //[ActionName("LoginPostAsync")]
        //public async Task<IActionResult> LoginPostAsync(LoginViewModel viewModel)
        //{
        //    var tokenResponse = await _userService
        //                        .LoginAsync(viewModel.Email, viewModel.Password);
        //    Response.Cookies.Append(
        //        TokenConstant.XAccessToken,
        //        tokenResponse.Token, new CookieOptions
        //        {
        //            HttpOnly = true,
        //            SameSite = SameSiteMode.Strict
        //        });
        //    return RedirectToAction("anasayfa", "yonetim");
        //}

        #endregion

        #region Kullanıcı Metotları

        [Authorize]
        public IActionResult kullanicilar()
        {
            var values = _mapper.Map<List<UserListItemDto>, List<UserListViewModel>>(_userService.UserList());
            return View(values);
        }

        [Authorize]
        public IActionResult kullanicikayit()
        {
            return View(new UserCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> kullaniciolustur(UserCreateViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Password == model.rePassword)
                    {
                        if (await _userService.Register(_mapper.Map<UserCreateViewModel, UserDto>(model)))
                        {
                            return RedirectToAction(nameof(kullanicilar));
                        }
                        else
                        {
                            return View(model);
                        }

                    }
                    else
                    {
                        return View(model);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {

                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        public IActionResult kullanicisil(int id)
        {
            if (!_userService.DeleteUserById(id))
            {
                return RedirectToAction(nameof(kullanicilar));
            }
            else
            {
                return RedirectToAction(nameof(kullanicilar));
            }
        }

        [HttpGet]
        public IActionResult kullaniciguncelle(int Id)
        {
            try
            {
                var getUser = _mapper.Map<UserDto, UserEditViewModel>(_userService.GetUserById(Id));
                return View(getUser);
            }
            catch (Exception ex)
            {

                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("anasayfa", "yonetim");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> kullaniciguncelle(UserEditViewModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (model.Password != "" || model.Password != null)
                    {

                        if (await _userService.UpdateUser(_mapper.Map<UserEditViewModel, UserDto>(model)))
                        {
                            return RedirectToAction(nameof(kullanicilar));
                        }

                        return View(model);

                    }
                    else
                    {
                       
                        if (await _userService.UpdateUser(_mapper.Map<UserEditViewModel, UserDto>(model)))
                        {
                            return RedirectToAction(nameof(kullanicilar));
                        }

                        return View(model);
                    }

                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = ex.ToString();
                return RedirectToAction("anasayfa", "yonetim");
            }

        }

        #endregion

        #region Rol Metotları

        [Authorize]
        public IActionResult roller()
        {
            var values = _mapper.Map<List<RoleListItemDto>, List<RolListViewModel>>(_roleService.roleList());
            return View(values);
        }

        [Authorize]
        public IActionResult roleekle()
        {
            return View(new RolCreateViewModel());
        }

        [Authorize]
        public IActionResult rolguncelle(int id)
        {
            var value = _mapper.Map<RoleDto, RolEditViewModel>(_roleService.getRoleById(id));

            if (value != null)
                return View(value);
            else
                return RedirectToAction("anasayfa", "yonetim", new { area = "editor" });
        }

        [HttpPost]
        public async Task<IActionResult> rolguncelle(RolEditViewModel model)
        {
            if (model != null)
            {
                bool result = await _roleService.updateRole(_mapper.Map<RolEditViewModel, RoleDto>(model));
                if (result != false)
                    return RedirectToAction("roller", "kullanici", new { area = "editor" });
                else
                    return RedirectToAction("roller", "kullanici", new { area = "editor" });
            }
            return View();
        }

        [Authorize]
        public IActionResult rolsil(int id)
        {
            if (_roleService.deleteRole(id))
                return RedirectToAction(nameof(roller));
            else
                return RedirectToAction(nameof(roller));
        }

        [HttpPost]
        public async Task<IActionResult> rololustur(RolCreateViewModel model)
        {
            if (model != null)
            {
                bool result = await _roleService.insertRole(_mapper.Map<RolCreateViewModel, RoleDto>(model));

                if (result != false)
                    return RedirectToAction(nameof(roleekle));
            }
            return RedirectToAction(nameof(roleekle));
        }

        [HttpGet]
        public IActionResult rolata(int id)
        {

            var value = _mapper.Map<UserDto, UserEditViewModel>(_userService.GetUserById(id));
            var roles = _roleService.roleList();
            TempData["userId"] = id;
            var userRoles = _roleService.ListRoleUserByUser(value.Id);
            List<RolAssignViewModel> model = new List<RolAssignViewModel>();

            for (int i = 0; i < roles.Count; i++)
            {

                RolAssignViewModel newModel = new RolAssignViewModel
                {
                    roleId = roles[i].Id,
                    name = roles[i].roleName,
                };

                for (int r = 0; r < userRoles.Count; r++)
                {
                    if (userRoles[r].roleId == roles[i].Id)
                    {
                        newModel.exists = true;
                        model.Add(newModel);
                    }

                }

                if (newModel.exists == false)
                {
                    model.Add(newModel);
                }

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> rolatamasiyap(List<RolAssignViewModel> model, int id)
        {

            foreach (var item in model)
            {
                if (item.exists)
                {
                    await _roleService.addRoleToUser(id, item.roleId);
                }
                else
                {
                    await _roleService.removeRoleToUser(id, item.roleId);
                }
            }

            return RedirectToAction(nameof(kullanicilar));
        }

        #endregion

        #region Extends

        public string rolesNameList(int id)
        {
            List<string> list = new List<string>();

            var value = _mapper.Map<UserDto, UserEditViewModel>(_userService.GetUserById(id));
            var roles = _roleService.roleList();
            var userRoles = _roleService.ListRoleUserByUser(value.Id);

            for (int i = 0; i < roles.Count; i++)
            {

                RolAssignViewModel newModel = new RolAssignViewModel
                {
                    roleId = roles[i].Id,
                    name = roles[i].roleName,
                };

                for (int r = 0; r < userRoles.Count; r++)
                {
                    if (userRoles[r].roleId == roles[i].Id)
                    {
                        newModel.exists = true;
                        list.Add(newModel.name);
                    }

                }
            }

            string[] roleNames = list.ToArray();
            string name = "";

            for (int i = 0; i < roleNames.Count(); i++)
            {
                name += "," + roleNames[i];
            }

            return name;
        }

        #endregion

    }
}
