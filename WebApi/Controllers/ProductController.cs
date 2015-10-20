using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        //Get All the products
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        //Add a product
        public Product PostProduct(Product item)
        {
            return repository.Add(item);
        }

        //Update a product
        public IEnumerable<Product> PutProduct(int id, Product product)
        {
            product.Id = id;
            if (repository.Update(product))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }

        //Delete a product
        public bool DeleteProduct(int id)
        {
            if (repository.Delete(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}