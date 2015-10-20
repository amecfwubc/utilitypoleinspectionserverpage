using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {           
            Add(new Product { Name = "Floppy Disk", Category = "Hardware/Electronics", Price = 20.10M });            
            Add(new Product { Name = "BMW", Category = "Car", Price = 3400000});
        }

        public IEnumerable<Product> GetAll()
        {           
            return products;
        }

        public Product Get(int id)
        {           
            return products.Find(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            item.Id = _nextId++;
            products.Add(item);

            return item;
        }

        public bool Update(Product item)
        {
            int index = products.FindIndex(p => p.Id == item.Id);           
            products.RemoveAt(index);
            products.Insert(index, item);
            return true;
        }

        public bool Delete(int id)
        { 
            products.RemoveAll(p => p.Id == id);
            return true;
        }
    }
}