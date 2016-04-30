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
        public Vote()
        {
            VoteItems = new List<VoteItem>();
        }
        [Key]
        public int VoteID { get; set; }
        public string User { get; set; } // string ?
        public int Week { get; set; }
        public ICollection<VoteItem> VoteItems { get; set; }
    }
}
