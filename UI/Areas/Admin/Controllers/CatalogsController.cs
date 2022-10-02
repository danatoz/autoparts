﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using DAL.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UI.Areas.Public.Controllers;
using Common.Enums;
using UI.Models;
using UI.Models.ViewModels;
using Hierarchy;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UI.TreeModel;
using Microsoft.VisualStudio.GraphModel;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UI.Models.ViewModels.FilterModel;
using Tools;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(AuthenticationSchemes = nameof(AuthScheme.Admin))]
	public class CatalogsController : BaseController
	{
		private readonly ILogger<CatalogsController> _logger;
		private readonly ApplicationDbContext _context;

		public CatalogsController(ILogger<CatalogsController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index(CatalogFilterModel filter, int page = 1)
		{
			const int objectsPerPage = 30;
			var startIndex = (page - 1) * objectsPerPage;
			var source = _context.Catalogs.Where(item => item.Active);
			var count = await source.CountAsync();
			var items = await source.Skip(startIndex).Take(objectsPerPage).ToListAsync();

			var viewModel = new SearchResultViewModel<CatalogModel, CatalogFilterModel>(CatalogModel.ConvertListFromDal(items), filter, count, 1, 1, objectsPerPage);
			return View(viewModel);
		}

		public async Task<IActionResult> Update(int? id)
		{
			await InitViewBag();

			var viewModel = CatalogModel.ConvertFromDal(
				await _context.Catalogs.FirstOrDefaultAsync(item => item.Id == id)) ?? new CatalogModel();

			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Update(CatalogModel model)
		{
			await InitViewBag();
			if (!ModelState.IsValid)
				return View(model);

			_context.Catalogs.Update(CatalogModel.ConvertToDal(model));
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Catalogs", new {Area = "Admin"});
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var model = await _context.Catalogs.FirstOrDefaultAsync(item => item.Id == id);
				model.Active = false;
				_context.Catalogs.Update(model);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
			}

			return RedirectToAction("Index", "Catalogs", new {Area = "Admin"});
		}
		private async Task InitViewBag()
		{
			ViewBag.CatalogCategory = new List<SelectListItem>()
			{
				new SelectListItem("--Не выбрано---", "")
			}.Concat(_context.Catalogs.Select(item => new SelectListItem(item.Name, item.Id.ToString()))).ToList();
		}
	}
}