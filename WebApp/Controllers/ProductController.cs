using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> List()
		{
			var productClient = new ProductAPI("https://localhost:7036", new HttpClient());
			var product = await productClient.GetProductsAsync();

			return View(product);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			var productClient = new ProductAPI("https://localhost:7036", new HttpClient());
			await productClient.CreateAsync(product);

			return RedirectToAction("List");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var productClient = new ProductAPI("https://localhost:7036", new HttpClient());
			var result = await productClient.GetProductByIdAsync(id);
			
			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			var productClient = new ProductAPI("https://localhost:7036", new HttpClient());
			await productClient.UpdateAsync(product);

			return RedirectToAction("List");
		}
	}
}
