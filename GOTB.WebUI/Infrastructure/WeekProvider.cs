using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTB.WebUI.Infrastructure
{
    public class WeekProvider : IWeekProvider
    {
        public int GetCurrentWeek()
        {
            var startOfAllTime = DateTime.Parse("28/04/2016");
            var passedTime = DateTime.Now - startOfAllTime;
            return passedTime.Days / 7;
        }
    }
}