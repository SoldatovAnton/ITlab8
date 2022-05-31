using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectricsOnlineWebApp.Models;

namespace ElectricsOnlineWebApp.Controllers
{
    public class CheckoutController : BaseController
    {
        private List<object> states;
        private List<object> cards;

        public CheckoutController()
        {
            states = new List<object> {
                new { SID = "Kir", SName = "Kirovsky" },
                new { SID = "Len", SName = "Leninsky" },
                new { SID = "Okt", SName = "Oktyabrsky" },
                new { SID = "Fac", SName = "Factory" },
                
            };

            cards = new List<object> {
                new { Type = "VISA" },
                new { Type = "Master Card" },
                new { Type = "MIR" }
            };

        }
        
        // GET: Checkout
        public ActionResult Index()
        {
            if (Session["test"] == null)
            {
                CartBasket cart = new CartBasket(1,"eater",10,1);
                List<CartBasket> carts = new List<CartBasket>(){cart};
                Session["test"] =carts;
            }

            ViewBag.kart = Session["test"];

          
            return View();
        }
        
        public JsonResult QuanityChange(int type, int pId)
        {
            ElectricsOnlineEntities context = new ElectricsOnlineEntities();
            List<CartBasket> carts = (List<CartBasket>)Session["test"];
            CartBasket product = null;
            foreach (var item in carts)
            {
                if (item.PID == pId)
                {
                    product = item;
                }
            }
            if (product == null)
            {
                return Json(new { d = "0" });
            }

            Product actualProduct = context.Products.FirstOrDefault(p => p.PID == pId);
            int quantity;
            // если 0, уменьшаем quantity
            // если 1, увеличиваем quanity
            switch (type)
            {
                case 0:
                    if (product.Quantity == 0) {
                        actualProduct.UnitsInStock++;
                        break;
                    }
                    product.Quantity--;
                    actualProduct.UnitsInStock++;
                    break;
                case 1:
                    product.Quantity++;
                    actualProduct.UnitsInStock--;
                    break;
                case -1:
                    actualProduct.UnitsInStock += product.Quantity;
                    product.Quantity = 0;
                    break;
                default:
                    return Json(new { d = "0" });
            }

            if (product.Quantity == 0)
            {
                carts.Remove(product);
                quantity = 0;
            }
            else
            {
                quantity = product.Quantity;
            }
            Session["test"] = carts;
            ViewBag.kart = carts;
            context.SaveChanges();
            return Json(new { d = quantity });
        }
        
        [HttpGet]
        public JsonResult UpdateTotal()
        {
            ElectricsOnlineEntities context = new ElectricsOnlineEntities();
            decimal total = 0;
            
            try
            {
                List<CartBasket> carts = (List<CartBasket>)Session["test"];
                
                foreach (var item in carts)
                {
                    total += item.UnitPrice * item.Quantity;
                }
               
            }
            catch (Exception) { total = 0; }

            return Json(new { d = String.Format("{0:c}", total) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clear()
        {
            try
            {
                List<ShoppingCartData> carts = (List<ShoppingCartData>)Session["test"];
                carts.ForEach(a => {
                    Product product = _ctx.Products.FirstOrDefault(p => p.PID == a.PID);
                    product.UnitsInStock += a.Quantity;
                });
                ViewBag.kart = new List<CartBasket>(){};
                Session["test"] = new List<CartBasket>(){};
                _ctx.SaveChanges();
            }
            catch (Exception) { }
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Purchase()
        {
            ViewBag.States = states;
            ViewBag.Cards = cards;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(ElectricsOnlineWebApp.Models.Customer customer)
        {
            ViewBag.States = states;
            ViewBag.Cards = cards;

            if (ModelState.IsValid)
            {
                if (customer.ExpDate <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Credit card has already expired");
                }

                if (customer.Ctype == "AMEX")
                {
                    if (customer.CardNo.Length != 15)
                    {
                        ModelState.AddModelError("", "AMEX must be 15 digits");
                    }
                }
                else
                {
                    if (customer.CardNo.Length != 16)
                    {
                        ModelState.AddModelError("", customer.Ctype + "must be 16 digits");
                    }
                }

                if (ModelState.IsValid)
                {
                    Customer c = new Customer
                    {
                        FName = customer.FName,
                        LName = customer.LName,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Address1 = customer.Address1,
                        Address2 = customer.Address2,
                        Suburb = customer.Suburb,
                        Postcode = customer.Postcode,
                        State = customer.State,
                        Ctype = customer.Ctype,
                        CardNo = customer.CardNo,
                        ExpDate = customer.ExpDate
                    };

                    Order o = new Order
                    {
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.Now.AddDays(5),
                        CID = c.CID
                    };

                    _ctx.Customers.Add(c);
                    _ctx.Orders.Add(o);
                    ViewBag.kart = (List<CartBasket>)Session["test"];
                    foreach (var i in ViewBag.kart)
                    {
                        _ctx.Order_Products.Add(new Order_Products
                        {
                            OrderID = o.OrderID,
                            PID = i.PID,
                            Qty = i.Quantity,
                            TotalSale = i.Quantity * i.UnitPrice
                        });
                       
                      
                    }
                    ViewBag.kart= new List<CartBasket>(){};
 Session["test"] = ViewBag.kart;
 try
 {
 
 _ctx.SaveChanges();
 
 }
 catch (DbEntityValidationException ex)
 {
     foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
     {
         Console.WriteLine("Object: "+validationError.Entry.Entity.ToString());
         Console.WriteLine(" ");
         foreach (DbValidationError err in validationError.ValidationErrors)
         {
             Console.WriteLine(err.ErrorMessage + " ");
         }
     }
 }

                    return RedirectToAction("PurchasedSuccess");

                }
            }

            List<ModelError> errors = new List<ModelError>();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors.Add(error);
                }
            }
            return View(customer);
        }

        public ActionResult PurchasedSuccess()
        {
            return View();
        }
    }
}
