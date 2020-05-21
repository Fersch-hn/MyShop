using LightInject;
using MyShop.Core.Models.Auth;
using MyShop.DataAccess.SQL.DbContextScope.Implementations;
using MyShop.DataAccess.SQL.DbContextScope.Interfaces;
using MyShop.DataAccess.SQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class ServiceRegister : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            var ambientDbContextLocator = new AmbientDbContextLocator();

            container.Register<IDbContextScopeFactory>((x) => new DbContextScopeFactory(null));
            container.Register<IAmbientDbContextLocator, AmbientDbContextLocator>(new PerScopeLifetime());

            container.Register<IRepository<ApplicationUser>>((x) => new Repository<ApplicationUser>(ambientDbContextLocator));
            container.Register<IRepository<ApplicationRole>>((x) => new Repository<ApplicationRole>(ambientDbContextLocator));
            

            
            container.Register<IUserService, UserService>();
        }
    }
}
