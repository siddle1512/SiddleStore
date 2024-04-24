using BusinessObject;
using DataAcess.Repository.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiddleStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
        }

        [Authorize(Roles = "Manager, Employee")]
        [HttpGet]
        public IActionResult GetProductList(string? search, int page = 1, int pageSize = 10)
        {
            try
            {
                var products = productRepository.GetProductList();

                if (!string.IsNullOrEmpty(search))
                {
                    products = productRepository.SearchProduct(search, products);
                }

                var totalCount = products.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalCount * pageSize);

                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return Ok(productsPerPage);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }          
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(ProductObject product)
        {
            try
            {
                productRepository.AddProduct(product);
                return Ok(new { mess = "Add product successfully!" });
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("EditProduct")]
        public IActionResult EditProduct(int id, ProductObject product)
        {
            try
            {
                product.ProductId = id;
                productRepository.Update(product);

                return Ok(new { mess = "Update Product with the ID" + id + "successfully!" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int? id) 
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Product ID is not found!");
                }
                productRepository.Delete(id.Value);

                return Ok( new { mess = "Delete product successfully!"});      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
