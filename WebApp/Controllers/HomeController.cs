using Microsoft.AspNetCore.Mvc;
using SUT.Models;
using System.Diagnostics;
using WebApp;

namespace SUT.Controllers
{
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

		public async Task<IActionResult> Product()
		{
			var productClient = new ProductAPI("https://localhost:7036", new HttpClient());
			var result = await productClient.GetProductsAsync();

			return View(result);
		}

		public IActionResult Demo()
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