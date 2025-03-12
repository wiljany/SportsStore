using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductsRepository repository;

        public AdminController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View(repository.Products);
        }
    }
}