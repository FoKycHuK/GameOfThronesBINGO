using System.Linq;
using System.Web.Mvc;
using GoTB.Domain.Abstract;
using GoTB.WebUI.Infrastructure;
using GoTB.WebUI.Infrastructure.Abstract;
using GoTB.WebUI.Models;

namespace GoTB.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository repo;
        private IAuthProvider authProvider;
        private IUserProvider userProvider;

        public AccountController(IUserRepository userRepo, IAuthProvider auth, IUserProvider user)
        {
            repo = userRepo;
            authProvider = auth;
            userProvider = user;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (authProvider.Authenticate(model.UserName, model.Password))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Неправильный логин или пароль";
                return View();
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            authProvider.Logout();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult QuickLogin()
        {
            return PartialView((object)userProvider.GetUserName(this));
        }

        [HttpPost]
        public ActionResult QuickLogin(string userName, string password)
        {
            if (authProvider.Authenticate(userName, password))
                return PartialView((object)userName);
            ViewBag.Error = "Неверное имя пользователя или пароль";
            return PartialView((object)userProvider.GetUserName(this));
        }

        public PartialViewResult AdminPanel()
        {
            var userName = userProvider.GetUserName(this);
            var user = repo.UserProfiles.FirstOrDefault(u => u.UserName == userName);
            return PartialView(user != null && user.IsAdmin);
        }
    }
}