﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTB.WebUI.Infrastructure
{
    public interface IWeekProvider
    {
        int GetCurrentWeek();
    }
}