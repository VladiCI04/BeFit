using BeFit.Services.Data.Interfaces;
using BeFit.Services.Data.Models.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BeFit.WebApi.Controllers
{
	[Route("api/statistics")]
	[ApiController]
	public class StatisticsApiController : ControllerBase
	{
		private readonly IEventService eventService;

        public StatisticsApiController(IEventService eventService)
        {
            this.eventService = eventService;
        }

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetStatistics()
		{
			try
			{
				StatisticsServiceModel serviceModel = await this.eventService.GetStatisticsAsync();

				return this.Ok(serviceModel);
			}
			catch (Exception)
			{
				return this.BadRequest();
			}
		}
    }
}
