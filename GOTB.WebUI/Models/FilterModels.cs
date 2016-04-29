using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoTB.WebUI.Controllers;

namespace GoTB.WebUI.Models
{
    public enum FilterBy
    {
        NoFilter,
        Alive,
        PriceLessThenThree
    }

    public class FilterModels
    {
        public static readonly Dictionary<FilterBy, string> filterDescribtion = new Dictionary<FilterBy, string>
        {
            {FilterBy.NoFilter, "Без фильтра"},
            {FilterBy.Alive, "Показать только живых"},
            {FilterBy.PriceLessThenThree, "Показать только тех, кто стоит меньше 3"}
        };
    }
}