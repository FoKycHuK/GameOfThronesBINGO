using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.WebUI.Models;
using GoTB.Domain.Entities;

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
        public int PageSize = 2;
        public Func<Character, int> OrderFunc = c => c.Id;
        public HomeController(ICharacterRepository characterRepository)
        {
            this.repository = characterRepository;
        }

        public ViewResult Index(int page = 1, FilterBy filter = FilterBy.NoFilter)
        {
            
            var characters = repository.Characters
                .Where(filters[filter])
                .ToArray();
            var charterersOnPage = characters
                .Skip((page - 1)*PageSize)
                .Take(PageSize)
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

            //return new ContentResult {Content = characters.Length.ToString()};
            return View(model);
        }
    }
}