using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Admin.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

		[Route("User/All")]
        public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> viewModel = await this.userService.AllAsync();

			return this.View(viewModel);
		}
	}
}
