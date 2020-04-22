using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace MyShop.Core.ViewModels
{
    public class ListUserViewModel
    {
        public IEnumerable<Customer> Users { get; set; }
        public ClaimsPrincipal UserNames {get; set;}

    }
}