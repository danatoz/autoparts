﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
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
	[Authorize(Roles = "admin")]
	public class CategoryController : BaseController
	{
		private readonly ILogger<CategoryController> _logger;
		private readonly AppDbContext _context;

		public CategoryController(ILogger<CategoryController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index(CategoryFilterModel filter, int page = 1)
		{
			const int objectsPerPage = 30;
			var startIndex = (page - 1) * objectsPerPage;
			var source = _context.Categories.Where(item => item.Active);
			var count = await source.CountAsync();
			var items = await source.Skip(startIndex).Take(objectsPerPage).ToListAsync();

			var viewModel = new SearchResultViewModel<Category, CategoryFilterModel>(items, filter, count, startIndex, items.Count, objectsPerPage);
			return View(viewModel);
		}

		public async Task<IActionResult> Update(Guid? id)
		{
			await InitViewBag();

			var viewModel = 
				await _context.Categories.FirstOrDefaultAsync(item => item.Id == id) ?? new Category();

			return View(viewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Update(Category model)
		{
			await InitViewBag();
			if (!ModelState.IsValid)
				return View(model);

			_context.Categories.Update(model);
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Category", new { Area = "Admin" });
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				var model = await _context.Categories.FirstOrDefaultAsync(item => item.Id == id);
				model.Active = false;
				_context.Categories.Update(model);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex}");
			}

			return RedirectToAction("Index", "Category", new { Area = "Admin" });
		}
		private async Task InitViewBag()
		{
			ViewBag.CatalogCategory = new List<SelectListItem>()
			{
				new SelectListItem("--Не выбрано---", "")
			}.Concat(_context.Categories.Select(item => new SelectListItem(item.Name, item.Id.ToString()))).ToList();
		}
	}
}
