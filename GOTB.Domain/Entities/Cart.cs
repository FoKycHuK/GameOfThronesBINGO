using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTB.Domain.Entities
{
    public class Cart
    {
        public const int StartPoints = 50;
        public List<int> CharacterIds;
        public int Points;

        public Cart()
        {
            CharacterIds = new List<int>();
            Points = StartPoints;
        }
    }
}
