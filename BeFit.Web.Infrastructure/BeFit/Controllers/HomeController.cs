﻿using BeFit.Services.Data.Interfaces;
using BeFit.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService eventService;

        public HomeController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel = await this.eventService.AllEventsAsync();
                
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }
           
            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return this.View();
        }
    }
}