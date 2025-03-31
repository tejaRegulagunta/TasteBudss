using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TasteBuds.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace TasteBuds.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagementController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)   // Constructer for the values that include in the readonly method
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
    
        // Get:Register method
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View(new RegisterPage());
        }
        //Post:Register method
        [HttpPost]
        public async Task<IActionResult> Register(RegisterPage model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                { 
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }
                    //assign the role
                    string role = model.Role ?? "User"; //Default here is User
                    if (role == "Admin" && !User.IsInRole("Admin"))
                    {
                        ModelState.AddModelError("", "Only for Admin");
                        return View(model);
                    }
                    await _userManager.AddToRoleAsync(user, role);
                    //assign Claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Email,model.Email),
                        new Claim(ClaimTypes.Role,"User")// custom role
                    };
                    await _userManager.AddClaimsAsync(user, claims);
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View(new LoginPage());
        }
        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginPage model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && model.Password != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,isPersistent:true, false);
                    //Assign Claims and Roles
                    if (result.Succeeded)
                    {
                        // Get user claims
                        var userClaims = await _userManager.GetClaimsAsync(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        // Create Claims Identity
                        var claimsIdentity = new ClaimsIdentity(userClaims, "ApplicationCookie");
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, model.Email));
                        foreach (var role in roles)
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                        }
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        // Sign in with the claims
                        await HttpContext.SignInAsync(claimsPrincipal);
                        // Redirect based the on role
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index","Product");
                        
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }
        // Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

