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
using switcher.controles;

namespace switcher.clases
{
    public class adaptadoresProvider
    {
        private static List<InfoAdapter> interfaces = new List<InfoAdapter>();
        public static List<InfoAdapter> getIstances()
        {

            interfaces.Clear();

            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapter");
            //mc = new ManagementClass("MSFT_NetAdapter");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //if (!string.IsNullOrEmpty((string)mo["NetConnectionID"]))
                if (!string.IsNullOrEmpty((string)mo["NetConnectionID"]))
                {
                    interfaces.Add(new InfoAdapter() { 
                        descripcion=(string)mo["Description"],
                        estado=(Boolean)mo["NetEnabled"],
                        nombre=(string)mo["NetConnectionID"],
                        objeto=mo
                    });
                }
            }

            return interfaces;
        }
    }
}
