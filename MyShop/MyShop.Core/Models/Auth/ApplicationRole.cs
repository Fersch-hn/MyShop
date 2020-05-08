using Microsoft.AspNet.Identity.EntityFramework;

namespace MyShop.Core.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public bool Enabled { get; set; }
    }
}
