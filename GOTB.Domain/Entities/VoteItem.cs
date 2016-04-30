using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTB.Domain.Entities
{
    public class VoteItem
    {
        [Key]
        public int VoteItemId { get; set; }
        public Vote Vote { get; set; }
        public Character Character { get; set; }
        public int Position { get; set; }
    }
}
