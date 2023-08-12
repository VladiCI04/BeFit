using BeFit.Data.Models;
using BeFit.Web.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static BeFit.Common.GeneralApplicationConstants;
using static BeFit.Common.NotificationMessagesConstants;

namespace BeFit.Controllers
{
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IMemoryCache memoryCache;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMemoryCache memoryCache)
        {
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.memoryCache = memoryCache;
        }

        [HttpGet]
		public IActionResult Register()
		{
			return this.View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterFormModel model)
		{
			if (!ModelState.IsValid) 
			{ 
				return this.View(model);
			}

			ApplicationUser user = new ApplicationUser()
			{
				FirstName = model.FirstName,
				LastName = model.LastName
			};

			await this.userManager.SetEmailAsync(user, model.Email);
			await this.userManager.SetUserNameAsync(user, model.Email);

			IdentityResult result = await this.userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

				return this.View(model);
			}

            await this.signInManager.SignInAsync(user, false);
			this.memoryCache.Remove(UserCacheKey);

            return this.RedirectToAction("Index", "Home");
        }

		[HttpGet]
		public async Task<IActionResult> Login(string? returnUrl = null)
		{
			await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			LoginFormModel model = new LoginFormModel()
			{
				ReturnUrl = returnUrl,
			};

			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginFormModel model)
		{
			if (!ModelState.IsValid)
			{
				return this.View(model);
			}

            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

			if (!result.Succeeded)
			{
				this.TempData[ErrorMessage] = "There was an error while logging you in! Please try again later or contact an administrator.";

				return this.View(model);
			}

			return this.Redirect(model.ReturnUrl ?? "/Home/Index");
		}
	}
}
