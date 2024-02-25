using Microsoft.AspNetCore.Mvc;
using WebApp.Producer;

namespace WebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService productService;

		public ProductController(IProductService productService)
		{
			this.productService = productService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> List()
		{
			var products = await productService.GetProducts();

			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			await productService.CreateProduct(product);

			return RedirectToAction("List");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var result = await productService.GetProductById(id);

			return View(result);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			await productService.EditProduct(product);

			return RedirectToAction("List");
		}
	}
}
