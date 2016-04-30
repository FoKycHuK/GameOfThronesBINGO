using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTB.WebUI.Models
{
    public class ButtonViewModel
    {
        public bool NeedToShowButton { get; set; }
        public string Method { get; set; }
        public string Controller { get; set; }
        public string TextButton { get; set; }
        public string TextIfNotNeeded { get; set; }
        public Dictionary<string, string> HiddenParams { get; set; }
    }
}