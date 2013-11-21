using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;

namespace switcher.clases
{
    public class adaptadores
    {

        public Dictionary<string, interfaceStatus> interfaces { get; set; }

        public List<String> init()
        {
            interfaces = new Dictionary<string,interfaceStatus>();

            List<String> values = new List<String>();
            interfaces.Clear();
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapter");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (!string.IsNullOrEmpty((string)mo["NetConnectionID"]))
                {
                    interfaces.Add((string)mo["NetConnectionID"],new interfaceStatus()
                    {
                        nombre = (string)mo["NetConnectionID"],
                        descripcion = (string)mo["Description"],
                        estado = (bool)mo["NetEnabled"]
                    });
                }
            }

            return values;
        }
    }
}
