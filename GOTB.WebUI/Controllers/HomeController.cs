using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.Domain.Concrete;
using GoTB.WebUI.Models;
using GoTB.Domain.Entities;
using GoTB.WebUI.Infrastructure;

namespace GoTB.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private ICharacterRepository repository;
        private ICartProvider cartProvider;

        public int PageSize = 2;
        public Func<Character, int> OrderFunc = c => c.Id;
        public HomeController(ICharacterRepository characterRepository, ICartProvider cartProvider)
        {
            this.repository = characterRepository;
            this.cartProvider = cartProvider;
        }

        public ViewResult Index(int page = 1, FilterBy filter = FilterBy.NoFilter, string substring = null)
        {
            ViewBag.substring = substring;
            ViewBag.filter = filter;
            var characters = repository.Characters
                .Where(FilterModels.Filters[filter])
                .ToArray();
            if (!string.IsNullOrEmpty(substring))
                characters = characters.Where(c => c.Name.Contains(substring)).ToArray();
            var charterersOnPage = characters
                .Skip((page - 1)*PageSize)
                .Take(PageSize)
                .Select(c => new CharacterViewModel {Character = c,
                    VoteType = CartHelpers.GetVoteTypeForCharacter(c, cartProvider.GetCart(this))})
                .ToArray();
            CharactersListViewModel model = new CharactersListViewModel
            {
                Characters = charterersOnPage,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = characters.Length
                }
            };
            
            return View(model);
        }

        public PartialViewResult CharacterInfo(CharacterViewModel characterViewModel)
        {
            return PartialView(characterViewModel);
        }

        [HttpGet]
        public PartialViewResult WriteComment(Character chr)
        {
            return PartialView(chr);
        }

        [HttpPost]
        public PartialViewResult WriteComment(string commentText, int chrId)
        {
            var context = new EFDbContext();
            var user = User.Identity.IsAuthenticated ? User.Identity.Name : "Anon";
            var comment = new Comment()
            {
                Author = user,
                Text = commentText,
                Time = DateTime.Now
            };
            var charr = context.Characters.First(c => c.Id == chrId);
            charr.Comments.Add(comment);
            context.SaveChanges();
            return PartialView(charr);
        }
    }
}