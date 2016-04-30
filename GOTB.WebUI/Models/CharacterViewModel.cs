using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoTB.Domain.Entities;

namespace GoTB.WebUI.Models
{
    public class CharacterViewModel
    {
        public Character Character { get; set; }
        public VoteType VoteType { get; set; }
    }
}