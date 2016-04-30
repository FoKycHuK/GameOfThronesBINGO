using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.WebUI.Models;
using GoTB.Domain.Entities;
using GoTB.WebUI.Infrastructure;

namespace GoTB.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private Dictionary<FilterBy, Func<Character, bool>> filters = new Dictionary<FilterBy, Func<Character, bool>>
        {
            {FilterBy.NoFilter, c => true},
            {FilterBy.Alive, c => c.IsAlive},
            {FilterBy.PriceLessThenThree, c => c.Price < 3}
        };

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
                .Where(filters[filter])
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
    }
}