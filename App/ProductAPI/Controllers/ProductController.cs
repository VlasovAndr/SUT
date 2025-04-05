using Azure;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Errors;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository context)
        {
            productRepository = context;
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public ActionResult<ApiResponse> GetProductById(int id)
        {
            var product = productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
            }

            var responce = new ApiResponse(StatusCodes.Status200OK);
            responce.Result = product;

            return Ok(responce);
        }

        [HttpGet]
        [Route("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApiResponse> GetProducts()
        {
            var products = productRepository.GetAllProducts();
            var responce = new ApiResponse(StatusCodes.Status200OK);
            responce.Result = products;

            return Ok(responce);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> Create(Product product)
        {
            try
            {
                var createdProduct = productRepository.AddProduct(product);
                var responce = new ApiResponse(StatusCodes.Status201Created);
                responce.Result = createdProduct;

                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, responce);
            }
            catch (Exception ex)
            {
                var responce = new ApiResponse(StatusCodes.Status400BadRequest);
                responce.Message += ex.Message;
                return BadRequest(responce);
            }
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> Update(Product product)
        {
            try
            {
                var updatedProduct = productRepository.UpdateProduct(product);
                var responce = new ApiResponse(StatusCodes.Status200OK);
                responce.Result = product;
                return Ok(responce);
            }
            catch (Exception ex)
            {
                var responce = new ApiResponse(StatusCodes.Status400BadRequest);
                responce.Message += ex.Message;
                return BadRequest(responce);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<ApiResponse> Delete(int id)
        {
            try
            {
                productRepository.DeleteProduct(id);
                var responce = new ApiResponse(StatusCodes.Status200OK);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                var responce = new ApiResponse(StatusCodes.Status400BadRequest);
                responce.Message += ex.Message;
                return BadRequest(responce);
            }
        }
    }
}