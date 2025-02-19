using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private IProductsRepository myrepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.myrepository = productsRepository;
        }

        // GET: Product

        public int PageSize = 4;
        public ViewResult List(string category, int page = 1)
        {
            // Skip(int) - Ignores the specified number of items
            // and returns a sequence starting at the item after the last skipped item (if any)
            ProductListViewModel model = new ProductListViewModel
            {
                Products = myrepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = PageSize,
                    //TotalItems = myrepository.Products.Count()
                    TotalItems = category == null ?
                    myrepository.Products.Count() :
                    myrepository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category

            };
			
            return View(model);

			// Take() returns a sequence containing up to the specified number of items
		}
        //public ViewResult List()
        //{
        //    return View(myrepository.Products);
        //}
    }
}