using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models.Model;
using PruebaTecnica.Models.ViewModel;
using PruebaTecnica.Servicio.Interfaces;
using System.Security.Cryptography;

namespace PruebaTecnica.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IAccountLogin _account;
        private readonly SignInManager<UserLogin> _signInManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IUserStore<UserLogin> _userStore;
        private readonly UserManager<UserLogin> _userManager;
        public LoginController(JwtSettings jwtSettings,
            IAccountLogin account,
            ILogger<LoginController> logger,
            SignInManager<UserLogin> signInManager,
            IUserStore<UserLogin> userStore,
            UserManager<UserLogin> userManager)
        {
            _jwtSettings = jwtSettings;
            _account = account;
            _logger = logger;
            _signInManager = signInManager;
            _userStore = userStore;
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //[AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Usuario", new { area = "Admin" });
            }
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var TOKEN = new UsuarioTokens();
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
                var _user = await _account.GetUserAsync(username, password);
                if (_user != null)
                {
                    TOKEN = _account.GetToken(_user);
                    _user.AccessToken = TOKEN.Token;
                    _user.RefreshToken = GenerateRefreshToken();
                    await _account.UpdateUserAsync(_user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
            if (TOKEN.Token != null)
            {
                return RedirectToAction("Index", "Usuario", new { area = "Admin" });
                //return Ok(TOKEN);
            }
            return View("Index");
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(string username, string password)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var user = CreateUser();
            user.NombreUsuario = username;
            user.Contrasenia = password;
            await _userStore.SetUserNameAsync(user, username, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToAction("Index", "Login");
            }
            List<string> _errors = new();
            foreach (IdentityError error in result.Errors)
            {
                _errors.Add(error.Description);
            }
            //return RedirectToAction("Registrar", "Login");
            return View("Registrar", _errors);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return View("Index");
        }
        private UserLogin CreateUser()
        {
            try
            {
                //ViewBag.dada = "";

                return Activator.CreateInstance<UserLogin>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserLogin)}'. " +
                    $"Ensure that '{nameof(UserLogin)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
