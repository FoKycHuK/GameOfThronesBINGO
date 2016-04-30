using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoTB.WebUI.Infrastructure.Abstract
{
    public interface IUserProvider
    {
        string GetUserName(Controller controller);
        bool IsAuthentificated(Controller controller);
    }
}