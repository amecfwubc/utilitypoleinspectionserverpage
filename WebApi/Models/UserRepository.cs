using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Data.Entity;
using System.Collections;

namespace WebApi.Models
{
    public class UserRepository : IUserRepository
    {
        private PoleInfoEntities db = new PoleInfoEntities();

        public UserRepository()
        {           
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var data = (from p in db.UserInformations
                        join a in db.AspNetUsers on p.AspNetUserId equals a.Id
                        select new UserViewModel
                        {
                            Id = p.Id,
                            UserFullName = p.UserFullName,
                            AspNetRoleID = p.AspNetRoleID,
                            UserName = a.UserName,
                            UPassword = p.UPassword,
                            Email = p.Email,
                            PhoneNumber = p.PhoneNumber,
                            IsActive = p.IsActive,
                        }).ToList();


            return data;
        }
        public IEnumerable<UserViewModel> GetUser(string UserName, string Password)
        {
            var data = (from p in db.UserInformations.Where(p=>p.UPassword==Password)
                        join a in db.AspNetUsers.Where(p=>p.UserName==UserName) on p.AspNetUserId equals a.Id
                        select new UserViewModel
                        {
                            Id = p.Id,
                            UserFullName = p.UserFullName,
                            AspNetRoleID = p.AspNetRoleID,
                            UserName=a.UserName,
                            UPassword=p.UPassword,
                            Email = p.Email,
                            PhoneNumber = p.PhoneNumber,
                            IsActive = p.IsActive,
                        }).ToList();



            return data;
        }

        public UserInformation Get(int id)
        {

            var data = (from p in db.UserInformations
                        where p.Id==id
                        select p).FirstOrDefault();


            return data;
        }


    }
}