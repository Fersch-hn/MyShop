using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace MyShop.WebUI.Controllers
{
    public class SearchFilterSortController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public SearchFilterSortController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        public ActionResult Index(Guid? Category)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();


            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;


            return View(model);
        }

        public ActionResult Search(string search, Guid? SelectedCategory)
        {
            IEnumerable<Product> products = context.Collection();
            IEnumerable<ProductCategory> categories = productCategories.Collection();


            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()));

            }

            if (SelectedCategory.HasValue)
            {
                products = products.Where(p => p.Category == SelectedCategory.Value);
            }




            var model = new ProductListViewModel
            {
                Products = products,
                ProductCategories = categories,
                Search = search,
                SelectedCategory = SelectedCategory.HasValue ? SelectedCategory.Value : Guid.Empty
            };
        
         
            
            model.SelectedCategory = SelectedCategory.HasValue ? SelectedCategory.Value : Guid.Empty;


            return View(model);
        }
    }
}