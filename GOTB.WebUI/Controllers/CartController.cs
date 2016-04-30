using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.Domain.Entities;
using GoTB.WebUI.Infrastructure;
using GoTB.WebUI.Models;

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
            return View(GetChoosenChsViewModels());
        }

        [HttpPost]
        public ActionResult Manage(int id)
        {

            if (!repository.Characters.Any(c => c.Id == id))
            {
                ViewBag.Error = "Такого персонажа не существует!";
                return View(GetChoosenChsViewModels());
            }
            var price = repository.Characters.First(c => c.Id == id).Price;
            var cart = cartProvider.GetCart(this);
            if (cart.CharacterIds.Contains(id))
            {
                ViewBag.Error = "Вы уже проголосовали за этого участника!";
                return View(GetChoosenChsViewModels());
            }
            if (cart.Points < price)
            {
                ViewBag.Error = "У вас недостаточно очков!";
                return View(GetChoosenChsViewModels());
            }
            cart.Points -= price;
            cart.CharacterIds.Add(id);
            return View(GetChoosenChsViewModels());
        }

        [HttpPost]
        public ActionResult Submit()
        {
            return null;
        }

        public PartialViewResult CartInfo()
        {
            return PartialView(cartProvider.GetCart(this));
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var cart = cartProvider.GetCart(this);
            if (!cart.CharacterIds.Contains(id))
            {
                ViewBag.Error = "Вы не голосовали за этого героя";
                return RedirectToAction("Manage");
            }
            var price = repository.Characters.First(c => c.Id == id).Price;
            cart.CharacterIds.Remove(id);
            cart.Points += price;
            return RedirectToAction("Manage");
        }

        private CharacterViewModel[] GetChoosenChsViewModels()
        {
            var ids = cartProvider.GetCart(this).CharacterIds;
            var chs = ids.Select(id => repository.Characters.First(c => c.Id == id)) //todo:join
                .Select(c => new CharacterViewModel() {Character = c, VoteType = VoteType.AlreadyVoted})
                .ToArray(); 
            return chs;
        }
    }
}
