using WebApi.Models;
using System.Collections.Generic;
using System.Collections;

namespace WebApi.Models
{
    interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product Add(Product item);
        bool Update(Product item);
        bool Delete(int id);
    }

    interface IPoleRepository
    {
        IEnumerable<PoleInfoViewModel> GetAll();
        IEnumerable<PoleInfoViewModel> GetAll(string UserName);
        PoleInfo Get(int id);
        PoleInfo Add(PoleInfo item);
        bool Update(PoleInfo item);
        bool Delete(int id);
    }

    interface IUserRepository
    {
        IEnumerable<UserViewModel> GetAll();
        IEnumerable<UserViewModel> GetUser(string UserName, string Password);
        UserInformation Get(int id);
    }
}
