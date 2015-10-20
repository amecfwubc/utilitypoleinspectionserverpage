using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        static readonly IUserRepository repository = new UserRepository();

        //Get All the products
        public IEnumerable<UserViewModel> GetAllPoleInfo()
        {
            return repository.GetAll();
        }

        public IEnumerable<UserViewModel> GetAllPoleInfo(string UserName, string Password)
        {
            return repository.GetUser(UserName, Password);
        }

        //Get All the products
        public UserInformation Get(int id)
        {
            return repository.Get(id);
        }


    }
}