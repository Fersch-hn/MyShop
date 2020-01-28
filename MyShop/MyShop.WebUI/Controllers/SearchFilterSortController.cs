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
        public ActionResult Index(string Category = null)
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

        public ActionResult Search(string search, string Category = null)
        {
            List<Product> products = context.Collection().ToList();
            List<ProductCategory> categories = productCategories.Collection().ToList();
        

            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search)).ToList();
                categories = categories.Where(s => s.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();

            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }
    }
}