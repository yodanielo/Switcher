using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace switcher.clases
{
    public class interfaceStatus
    {
        public bool estado { get; set; }
        public string nombre { get; set; }
        public void enable() {
            estado = true;
            string cad = "/C netsh interface set interface \"" + nombre + "\" enabled";
            System.Diagnostics.Process.Start("CMD.exe", cad);
            //System.Diagnostics.Process.Start(cad);
        }
        public void disable()
        {
            estado = false;
            string cad = "/C netsh interface set interface \"" + nombre + "\" disabled";
            System.Diagnostics.Process.Start("CMD.exe", cad);
            //System.Diagnostics.Process.Start(cad);
        }
        public void toggle() {
            if (estado)
                disable();
            else
                enable();
        }

        public string descripcion { get; set; }

        public string controlName { get; set; }
    }
}
