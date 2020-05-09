using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RedisCache_App.Models;

namespace RedisCache_App.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private IDistributedCache redisCache;
		private BatchEVContext db;

		public HomeController(BatchEVContext db,IDistributedCache cache)
		{
			this.redisCache = cache;
			this.db = db;
		}
		public IActionResult Index()
		{
			string jsonCategories = redisCache.GetString("categories");
			if (jsonCategories == null)
			{
				ViewBag.Status = "Data Received from the Database";
				// get data from database
				List<Category> categories = db.Category.ToList();
				jsonCategories = JsonSerializer.
						Serialize<List<Category>>(categories);
				// save data in cache
				var options = new DistributedCacheEntryOptions();
				options.SetAbsoluteExpiration(DateTimeOffset.Now.AddMinutes(1));
				redisCache.SetString("categories", jsonCategories, options);
			}
			else
			{
				ViewBag.Status = "Data Received from the Cache";
			}

			JsonSerializerOptions opt = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true
			};
			List<Category> cats = JsonSerializer.Deserialize<List<Category>>(jsonCategories,opt);
			return View(cats);
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
	}
}
