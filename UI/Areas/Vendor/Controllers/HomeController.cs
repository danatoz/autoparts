﻿using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Common.Enums;
using UI.Models;

namespace UI.Areas.Vendor.Controllers
{
	[Area("Vendor")]
	[Authorize(AuthenticationSchemes = nameof(AuthScheme.Vendor))]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> Login()
		{

			return RedirectToAction("Index", "Home", new { Area = "Public" });
		}

		public async Task<IActionResult> Logout()
		{

			return RedirectToAction("Index", "Home", new { Area = "Public" });
		}
	}
}