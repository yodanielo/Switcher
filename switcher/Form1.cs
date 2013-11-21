using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using switcher.clases;

namespace switcher
{
    public partial class Form1 : Form
    {
        bool mostrado = false;
        bool puedesalir = false;
        adaptadores adap = new adaptadores();
        public Form1()
        {
            InitializeComponent();
            this.Hide();
            adap.init();

            int i = 0;
            foreach (var item in adap.interfaces)
            {
                ToolStripMenuItem menuitem = new ToolStripMenuItem()
                {
                    Name = "menu_" + i.ToString(),
                    Text = item.Value.nombre,
                    MergeIndex = 0
                };
                item.Value.controlName = menuitem.Name;
                cambiarIcono(item.Value.estado, menuitem);

                menuitem.Click += (s, e) => {
                    adap.interfaces[((ToolStripMenuItem)s).Text].toggle();
                    cambiarIcono(item.Value.estado, menuitem);
                };
                //menuIcon.Items.Add(menuitem);
                menuIcon.Items.Insert(0, menuitem);
            }
        }

        private void cambiarIcono(bool status, ToolStripMenuItem menu){
            if (status)
                menu.Image = global::switcher.Properties.Resources.statusok;
            else
                menu.Image = global::switcher.Properties.Resources.statusdown;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puedesalir = true;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!puedesalir)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mostrado)
                this.Hide();
            else
                this.Show();
            mostrado = !mostrado;
        }

        private void enableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adap.interfaces)
            {
                item.Value.enable();
                cambiarIcono(item.Value.estado,(ToolStripMenuItem)menuIcon.Items[item.Value.controlName]);
            }
        }

        private void disableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adap.interfaces)
            {
                item.Value.disable();
                cambiarIcono(item.Value.estado, (ToolStripMenuItem)menuIcon.Items[item.Value.controlName]);
            }
        }

        private void toggleAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adap.interfaces)
            {
                item.Value.toggle();
                cambiarIcono(item.Value.estado, (ToolStripMenuItem)menuIcon.Items[item.Value.controlName]);
            }
        }

    }
}
