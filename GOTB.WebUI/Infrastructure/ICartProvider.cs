using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Entities;

namespace GoTB.WebUI.Infrastructure
{
    public interface ICartProvider
    {
        Cart GetCart(Controller controller);
        void SetCart(Controller controller, Cart cart);
    }
}