using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectricsOnlineWebApp.Models;

namespace ElectricsOnlineWebApp.Controllers
{
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            if (Session["test"] == null)
            {
             /* CartBasket cart = new CartBasket(1,"eater",10,1);
                List<CartBasket> carts = new List<CartBasket>(){cart};*/
                Session["test"] =new List<CartBasket>(){};
            }

            ViewBag.kart = Session["test"];
            
            List<Product> products = _ctx.Products.ToList<Product>();
          
            ViewBag.Products = products;
            return View();
        }

        public ActionResult Category(string catName)
        {
            List<Product> products;
            if (catName == "")
            {
                products = _ctx.Products.ToList<Product>();
            } else { 
                products = _ctx.Products.Where(p => p.Category == catName).ToList<Product>();
            }
            ViewBag.Products = products;
            return View("Index");
        }

      

        public ActionResult AddToCart(int id)
        {
         
            
            addToCart(id);
            return RedirectToAction("Index");
        }
        private void addToCart(int pId)
        {
            Product product = _ctx.Products.FirstOrDefault(p => p.PID == pId);
            if (product != null && product.UnitsInStock > 0)
            {
                List<CartBasket> carts = (List<CartBasket>)Session["test"];
                CartBasket cart = null;
                foreach (var item in carts)
                {
                    if (item.PID == pId)
                    {
                        cart = item;
                    }
                } // смотрем нет ли уже продукта в корзине
                
                if (cart != null)
                {
                    cart.Quantity++;
                }
                else
                {

                    cart = new CartBasket(
                        product.PID,
                        product.PName,                       
                        product.UnitPrice,
                        1
                    );

                    carts.Add(cart);
                }

                Session["test"] = carts;
                ViewBag.kart = carts;
                
                product.UnitsInStock--;
                _ctx.SaveChanges();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "о нас .";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Наши контакты.";

            return View();
        }
    }
}