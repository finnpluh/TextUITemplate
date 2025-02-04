using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextUITemplate.Management
{
    public class Button
    {
        public string title { get; set; }
        public string tooltip { get; set; }
        public bool toggled { get; set; }
        public bool isToggleable { get; set; }
        public Action action { get; set; }
        public Action disableAction { get; set; }
    }
}
