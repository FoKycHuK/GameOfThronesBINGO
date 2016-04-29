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
        private ICharacterRepository repository;
        public int PageSize = 2;
        public Func<Character, int> OrderFunc = c => c.Id;
        public HomeController(ICharacterRepository characterRepository)
        {
            this.repository = characterRepository;
        }

        public ViewResult Index(int page = 1)
        {
            //return View(
            //    repository.Characters
            //    .OrderBy(c => c.Id)
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize)
            //    .ToArray());

            CharactersListViewModel model = new CharactersListViewModel
            {
                Characters = repository.Characters
                .OrderBy(OrderFunc)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Characters.Count()
                }
            };

            return View(model);
        }
    }
}