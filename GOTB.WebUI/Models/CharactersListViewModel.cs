using System.Collections.Generic;
using GoTB.Domain.Entities;

namespace GoTB.WebUI.Models
{
    public class CharactersListViewModel
    {
        public IEnumerable<CharacterViewModel> Characters { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}