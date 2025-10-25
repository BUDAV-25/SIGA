using Microsoft.AspNetCore.Mvc;
using SIGA.Application.Contracts.Entities;
using SIGA.Application.Dtos.Identity.Account;
using SIGA.Application.Helpers;
using SIGA.Application.Response;
using SocialNetwork.Web.Helpers.Perfil;

namespace SIGA.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly PerfilHelper _perfilHelper;

        public UsersController(IUserService userService, PerfilHelper perfilHelper)
        {
            _userService = userService;
            _perfilHelper = perfilHelper;
        }
        public ActionResult Home()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            AuthenticationResponse authentication = await _userService.LoginAsync(loginDto);

            if (authentication != null && authentication.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("usuario", authentication);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                loginDto.HasError = authentication.HasError;
                loginDto.Error = authentication.Error;
                return View(loginDto);
            }
        }

        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return View(registerDto);
            }

            var origen = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(registerDto, origen);

            if (!response.HasError)
            {
                registerDto = await _perfilHelper.LoadPhoto(registerDto, file);
            }

            if (registerDto.Foto != null)
            {
                await _userService.UploadPhotoAsync(registerDto);
            }

            if (response.HasError)
            {
                registerDto.HasError = response.HasError;
                registerDto.Error = response.Error;

                return View(registerDto);
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("usuario");

            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
