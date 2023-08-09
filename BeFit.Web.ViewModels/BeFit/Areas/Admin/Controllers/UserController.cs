using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static BeFit.Common.GeneralApplicationConstants;

namespace BeFit.Areas.Admin.Controllers
{
	public class UserController : BaseController
	{
		private readonly IUserService userService;
		private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
			this.memoryCache = memoryCache;	
        }

		[Route("User/All")]
		[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> users = this.memoryCache.Get<IEnumerable<UserViewModel>>(UserCacheKey);
			if (users == null)
			{
				users = await this.userService.AllAsync();

				MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(UsersCacheDurationMinutes));

				this.memoryCache.Set(UserCacheKey, users, cacheOptions);
			}

			return this.View(users);
		}
	}
}
