﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTB.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
