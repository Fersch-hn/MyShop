using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.WebUI.ViewModels
{
    public class ListUserViewModel
    {
        public IEnumerable<MyShop.WebUI.Models.ApplicationUser> Users { get; set; }
        public IEnumerable<string> roles { get; set; }
    }
}