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
        private IProductRepository repository;
        public int PageSize = 4;
        public HomeController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}