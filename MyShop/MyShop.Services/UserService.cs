using MyShop.Core.Models.Auth;
using MyShop.Core;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using MyShop.DataAccess.SQL;
using MyShop.DataAccess.SQL.DbContextScope.Interfaces;
using MyShop.DataAccess.SQL.Repository;
using MyShop.DataAccess.SQL.DbContextScope.Extensions;

namespace MyShop.Services
{
    public interface IUserService
    {
        IEnumerable<UserGrid> GetAll();
    }

    public class UserService : IUserService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<ApplicationUser> _applicationUserRepo;

        public UserService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<ApplicationUser> applicationUserRepo
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _applicationUserRepo = applicationUserRepo;
        }

        public IEnumerable<UserGrid> GetAll()
        {
            var result = new List<UserGrid>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var abc = _applicationUserRepo.GetAll().ToList();

                    var users = ctx.GetEntity<ApplicationUser>();
                    var roles = ctx.GetEntity<ApplicationRole>();
                    var usersPerRoles = ctx.GetEntity<ApplicationUserRole>();

                    var queryUsersPerRoles = (
                        from upr in usersPerRoles
                        from r in roles.Where(x => x.Id == upr.RoleId)
                        select new
                        {
                            upr.UserId,
                            r.Name
                        }
                    ).AsQueryable();

                    result = (
                        from u in users
                        select new UserGrid
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Role = queryUsersPerRoles.Where(x =>
                                x.UserId == u.Id
                            ).Select(x => x.Name).ToList()
                        }
                    ).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
    }
}

