using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.Domain.Entities;
using GoTB.WebUI.Infrastructure;
using GoTB.WebUI.Infrastructure.Abstract;
using GoTB.WebUI.Models;

namespace GoTB.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICharacterRepository repository;
        private readonly IVoteRepository voteRepository;
        private readonly ICartProvider cartProvider;
        private readonly IWeekProvider weekProvider;
        private readonly IUserProvider userProvider;

        public CartController(ICharacterRepository rep, IVoteRepository voteRepository,
            ICartProvider cartProvider, IWeekProvider weekProvider, IUserProvider userProvider)
        {
            repository = rep;
            this.cartProvider = cartProvider;
            this.weekProvider = weekProvider;
            this.voteRepository = voteRepository;
            this.userProvider = userProvider;
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

        [Authorize]
        [HttpPost]
        public ActionResult Submit()
        {
            var cart = cartProvider.GetCart(this);
            
            if (cart.CharacterIds.Count == 0)
                return RedirectToAction("Manage");
            
            var vote = new Vote();
            vote.User = userProvider.GetUserName(this);
            vote.Week = weekProvider.GetCurrentWeek();
            vote.VoteItems = GetVoteItems(cart.CharacterIds, vote);
            voteRepository.Add(vote);

            return RedirectToAction("Manage");
        }

        private List<VoteItem> GetVoteItems(List<int> charecterIds, Vote vote)
        {
            var counter = 0;
            var ans = new List<VoteItem>();
            foreach (var chId in charecterIds)
            {
                var chr = repository.Characters.First(x => x.Id == chId);
                var voteItem = new VoteItem
                {
                    Character = chr,
                    Position = counter,
                    Vote = vote
                };
                ans.Add(voteItem);
            }
            return ans;
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
            var choosen = voteRepository.Votes
                .FirstOrDefault(c => c.User == userProvider.GetUserName(this) &&
                                     c.Week == weekProvider.GetCurrentWeek());
            if (choosen != null)
                return choosen.VoteItems.Select(c =>
                    new CharacterViewModel() {Character = c.Character, VoteType = VoteType.AlreadyVoted})
                    .ToArray();
            var ids = cartProvider.GetCart(this).CharacterIds;
            var chs = ids.Select(id => repository.Characters.First(c => c.Id == id)) //todo:join
                .Select(c => new CharacterViewModel() {Character = c, VoteType = VoteType.AlreadyVoted})
                .ToArray();
            return chs;
        }

        public PartialViewResult SubmitButton(ButtonViewModel bvm)
        {
            if (bvm == null)
                bvm = new ButtonViewModel() {NeedToShowButton = false, TextIfNotNeeded = "Произошла ошибка."};
            var week = weekProvider.GetCurrentWeek();
            var userName = userProvider.GetUserName(this);
            if (bvm.NeedToShowButton)
                bvm.NeedToShowButton = !userProvider.IsAuthentificated(this) ||
                                       !voteRepository.Votes.Any(v => v.User == userName && v.Week == week);
            return PartialView(bvm);
        }
    }
}
