using Microsoft.AspNet.Identity.EntityFramework;

namespace Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public bool Enabled { get; set; }
    }
}
