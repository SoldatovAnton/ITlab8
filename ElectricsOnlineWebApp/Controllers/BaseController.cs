using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectricsOnlineWebApp.Models;

namespace ElectricsOnlineWebApp.Controllers
{
    public class BaseController : Controller
    {
        internal protected HttpSessionStateBase SharedSession;
        protected ElectricsOnlineEntities _ctx = new ElectricsOnlineEntities();
        
        public BaseController()
        {
            ViewBag.CartTotalPrice = CartTotalPrice;
           
            ViewBag.CartUnits = kart.Count;
            
            
           ViewBag.kart = kart;
            
          
}

        private List<CartBasket> kart
        {
            get
            {
                return new List<CartBasket>(){};
            }
        }

        private decimal CartTotalPrice
        {
            get
            {
                return kart.Sum(c => c.Quantity * c.UnitPrice);
            }
        }
        
    }
}