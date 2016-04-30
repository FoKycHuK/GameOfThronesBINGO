using System.Web.Mvc;
using GoTB.WebUI.Infrastructure.Abstract;
using GoTB.WebUI.Models;

namespace GoTB.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
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

    }
}