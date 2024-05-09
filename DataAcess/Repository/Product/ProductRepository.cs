using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;

        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public void AddProduct(ProductObject product) => _productDAO.AddProduct(product);
        public List<ProductObject> GetProductList(bool order = false) => _productDAO.GetProductsList(order);
        public ProductObject GetProduct(int productId, List<ProductObject>? searchList = null) => _productDAO.GetProduct(productId, searchList);
        public List<ProductObject> SearchProduct(string search, List<ProductObject> products) => _productDAO.SearchProduct(search, products);
        public void Update(ProductObject product) => _productDAO.Update(product);
        public void Delete(int productId) => _productDAO.Delete(productId);
    }
}
