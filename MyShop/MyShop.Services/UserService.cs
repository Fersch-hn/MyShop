using Models.Auth;
using MyShop.Core;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using MyShop.DataAccess.SQL;

namespace MyShop.Services
{
    public class UserService
    {
        public IEnumerable<UserGrid> GetAll()
        {
            var result = new List<UserGrid>();
            using (var ctx = new DataContext())
            {
                result = (
                    from au in ctx.ApplicationUsers
                    from aur in ctx.ApplicationUserRoles.Where(x => x.UserId == au.Id).DefaultIfEmpty()
                    from ar in ctx.ApplicationRoles.Where(x => x.Id == aur.RoleId && x.Enabled).DefaultIfEmpty()
                    select new UserGrid
                    {
                        Id = au.Id,
                        Name = au.FirstName,
                        LastName = au.LastName,
                        Email = au.Email,
                        Role = ar.Name
                    }

                ).ToList();
            }

            return result;
        }

        public ApplicationUser Get(string id)
        {
            var result = new ApplicationUser();
            using (var ctx = new DataContext())
            {
                result = ctx.ApplicationUsers.Where(x => x.Id == id).Single();
            }

            return result;
        }
    }
}

