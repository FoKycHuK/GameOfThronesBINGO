using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTB.Domain.Entities
{
    public class Vote
    {
        [Key]
        public int VoteID;
        public string User { get; set; } // string ?
        public int Week { get; set; }
        public List<VoteItem> VoteItems { get; set; }
    }
}
