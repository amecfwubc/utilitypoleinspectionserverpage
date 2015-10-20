using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PoleInfoController : ApiController
    {
        static readonly IPoleRepository repository = new PoleRepository();

        //Get All the products
        public IEnumerable<PoleInfoViewModel> GetAllPoleInfo()
        {
            return repository.GetAll();
        }

        public IEnumerable<PoleInfoViewModel> GetAllPoleInfo(string UserName)
        {
            return repository.GetAll(UserName);
        }

        //Get All the products
        public PoleInfo Get(int id)
        {
            return repository.Get(id);
        }

        //Add a PoleInfo
        public PoleInfo PostPoleInfo(PoleInfo item)
        {
            return repository.Add(item);
        }

        //Update a PoleInfo
        public IEnumerable<PoleInfoViewModel> PutPoleInfo(int id, PoleInfo PoleInfo)
        {
            PoleInfo.ID = id;
            if (repository.Update(PoleInfo))
            {
                return repository.GetAll();
            }
            else
            {
                return null;
            }
        }

        //Delete a PoleInfo
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