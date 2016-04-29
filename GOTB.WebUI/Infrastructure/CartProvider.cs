using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoTB.Domain.Entities;

namespace GoTB.WebUI.Infrastructure
{
    public class CartProvider : ICartProvider
    {
        public Cart GetCart(System.Web.Mvc.Controller controller)
        {
            return controller.Session["Cart"] as Cart;
        }

        public void SetCart(System.Web.Mvc.Controller controller, Cart cart)
        {
            controller.Session["Cart"] = cart;
        }
    }
}