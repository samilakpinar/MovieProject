using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SidebarMenu 
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public SidebarMenu(string title, string link)
        {
            this.Title = title;
            this.Link = link;
        }
    }
}
