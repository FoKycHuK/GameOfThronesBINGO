using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.WebUI.Infrastructure.Abstract;

namespace GoTB.WebUI.Infrastructure
{
    public class FormsUserProvider : IUserProvider
    {
        public string GetUserName(Controller controller)
        {
            return IsAuthentificated(controller) ? controller.User.Identity.Name : "";
        }

        public bool IsAuthentificated(Controller controller)
        {
            return controller.User.Identity.IsAuthenticated;
        }
    }
}