using BusinessObject;
using DataAcess.DAO;

namespace DataAcess.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        public void AddProduct(ProductObject product) => ProductDAO.Instance.AddProduct(product);
        public List<ProductObject> GetProductList(bool order = false) => ProductDAO.Instance.GetProductsList(order);
        public ProductObject GetProduct(int productId, List<ProductObject>? searchList = null) => ProductDAO.Instance.GetProduct(productId, searchList);
        public List<ProductObject> SearchProduct(string search, List<ProductObject> products) => ProductDAO.Instance.SearchProduct(search, products);
        public void Update(ProductObject product) => ProductDAO.Instance.Update(product);
        public void Delete(int productId) => ProductDAO.Instance.Delete(productId);
    }
}
