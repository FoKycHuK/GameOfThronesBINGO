using System.Web.Mvc;
using GoTB.WebUI.Infrastructure;
using GoTB.WebUI.Infrastructure.Abstract;
using GoTB.WebUI.Models;

namespace GoTB.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        private IUserProvider userProvider;

        public AccountController(IAuthProvider auth, IUserProvider user)
        {
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

                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
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

        public PartialViewResult UserNameView()
        {
            return PartialView((object)userProvider.GetUserName(this));
        }
    }
}