using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using switcher.clases;

namespace switcher.controles
{
    public class MenuNetIn : ToolStripMenuItem
    {
        public MenuNetIn()
        {
            adaptador = new InfoAdapter();
            Name = "MenuNetIn_" + numControl.ToString();
            numControl++;
        }
        private static int numControl = 0;
        public InfoAdapter adaptador { get; set; }
        
    }

}
