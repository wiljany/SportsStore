using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

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
        public ViewResult List()
        {
            return View(myrepository.Products);
        }
    }
}