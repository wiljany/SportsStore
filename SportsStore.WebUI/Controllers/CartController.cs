﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        public CartController(IProductsRepository repo)
        {
            this.repository = repo;
        }

        // get cart
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        // add to cart
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // remove from cart
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
			Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);

			if (product != null)
			{
				cart.RemoveLine(product);
			}
			return RedirectToAction("Index", new { returnUrl });
		}
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel 
            {
                // Cart = GetCart(),
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}