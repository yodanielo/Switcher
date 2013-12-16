using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace switcher.clases
{
    public class InfoAdapter
    {
        private ProcessStartInfo nn = new ProcessStartInfo();

        public bool estado { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public System.Management.ManagementObject objeto { get; set; }

        private void internalEnable()
        {
            objeto.InvokeMethod("Enable", null);
        }
        public void enable()
        {
            estado = true;
            //nn.FileName = "CMD.exe";
            //nn.WindowStyle = ProcessWindowStyle.Hidden;
            //nn.Arguments = "/C netsh interface set interface \"" + nombre + "\" enabled";
            //System.Diagnostics.Process.Start(nn);
            //estado = true;
            var tarea = new System.Threading.Thread(new System.Threading.ThreadStart(internalEnable));
            tarea.Start();
            //System.Diagnostics.Process.Start(cad);
        }
        private void internalDisable() {
            objeto.InvokeMethod("Disable", null);
        }
        public void disable()
        {
            estado = false;
            //nn.FileName = "CMD.exe";
            //nn.WindowStyle = ProcessWindowStyle.Hidden;
            //nn.Arguments = "/C netsh interface set interface \"" + nombre + "\" disabled";
            //System.Diagnostics.Process.Start(nn);

            //estado = false;
            var tarea = new System.Threading.Thread(new System.Threading.ThreadStart(internalDisable));
            tarea.Start();
            //System.Diagnostics.Process.Start(cad);
        }
        public void toggle()
        {
            if (estado)
                disable();
            else
                enable();
        }
    }
}
