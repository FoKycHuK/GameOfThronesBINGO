using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoTB.Domain.Entities;
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
        public static readonly Dictionary<FilterBy, string> FilterDescription = new Dictionary<FilterBy, string>
        {
            {FilterBy.NoFilter, "Без фильтра"},
            {FilterBy.Alive, "Показать только живых"},
            {FilterBy.PriceLessThenThree, "Показать только тех, кто стоит меньше 3"}
        };

        public static readonly Dictionary<FilterBy, Func<Character, bool>> Filters = new Dictionary<FilterBy, Func<Character, bool>>
        {
            {FilterBy.NoFilter, c => true},
            {FilterBy.Alive, c => c.IsAlive},
            {FilterBy.PriceLessThenThree, c => c.Price < 3}
        };
    }
}