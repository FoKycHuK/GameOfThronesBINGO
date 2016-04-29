using System.Collections.Generic;
using GoTB.Domain.Entities;

namespace GoTB.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Character> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}