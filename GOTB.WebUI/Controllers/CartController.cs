using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.Domain.Entities;
using GoTB.WebUI.Infrastructure;

namespace GoTB.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICharacterRepository repository;
        private readonly ICartProvider cartProvider;

        public CartController(ICharacterRepository rep, ICartProvider cartProvider)
        {
            repository = rep;
            this.cartProvider = cartProvider;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            return View(GetChoosenChs());
        }

        [HttpPost]
        public ActionResult Manage(int id)
        {

            if (!repository.Characters.Any(c => c.Id == id))
            {
                ViewBag.Error = "Такого персонажа не существует!";
                return View(GetChoosenChs());
            }
            var price = repository.Characters.First(c => c.Id == id).Price;
            var cart = GetCart();
            if (cart.CharacterIds.Contains(id))
            {
                ViewBag.Error = "Вы уже проголосовали за этого участника!";
                return View(GetChoosenChs());
            }
            if (cart.Points < price)
            {
                ViewBag.Error = "У вас недостаточно очков!";
                return View(GetChoosenChs());
            }
            cart.Points -= price;
            cart.CharacterIds.Add(id);
            return View(GetChoosenChs());
        }

        [HttpPost]
        public ActionResult Submit()
        {
            return null;
        }

        public PartialViewResult CartInfo()
        {
            return PartialView(GetCart());
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var cart = GetCart();
            var price = repository.Characters.First(c => c.Id == id).Price;
            cart.CharacterIds.Remove(id);
            cart.Points += price;
            return RedirectToAction("Manage");
        }

        private Character[] GetChoosenChs()
        {
            var ids = GetCart().CharacterIds;
            var chs = ids.Select(id => repository.Characters.First(c => c.Id == id)).ToArray(); //todo:join
            return chs;
        }

        Cart GetCart()
        {
            var cart = cartProvider.GetCart(this);
            if (cart != null) return cart;
            cart = new Cart();
            cartProvider.SetCart(this, cart);
            return cart;
        }
    }
}
