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
using switcher.controles;

namespace switcher
{
    public partial class Form1 : Form
    {
        bool mostrado = false;
        bool puedesalir = false;
        List<MenuNetIn> adaptadores = new List<MenuNetIn>();

        Timer escaneo = new Timer();


        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            escaneo.Interval = 5000;
            escaneo.Tick += escaneo_Tick;
            escaneo.Start();
            return;
        }

        private void escanear()
        {
            var adaps = adaptadoresProvider.getIstances();//esto te da todos los adaptadores

            //elimino los implementados que no estan en la lista deadaptadores
            var auxRemover = (from yy in adaps select yy.nombre);
            var aRemover = (from xx in adaptadores where !auxRemover.Contains(xx.adaptador.nombre) select xx).ToList();
            aRemover.ForEach(x =>
            {
                x.Dispose();
                adaptadores.Remove(x);
            });

            //checo el estado de los adaptadores
            var estadosCheck = (from xx in adaptadores join yy in adaps on xx.adaptador.nombre equals yy.nombre where xx.adaptador.estado != yy.estado select xx).ToList();
            estadosCheck.ForEach(x =>
            {
                x.adaptador.estado = !x.adaptador.estado;
                cambiarIcono(x.adaptador.estado, x);
            });

            //agrego los adaptadores que no estan en la lista
            var auxagregar = (from yy in adaptadores select yy.adaptador.nombre);
            var aAgregar = (from xx in adaps where !auxagregar.Contains(xx.nombre) select xx).ToList();
            aAgregar.ForEach(x =>
            {
                var adap = new MenuNetIn()
                {
                    adaptador = x,
                    Text = x.nombre
                };
                cambiarIcono(x.estado, adap);
                adap.Click += ItemAdapter_Click;
                adaptadores.Add(adap);
                menuIcon.Items.Insert(0, adap);
            });
            GC.Collect();
        }

        void escaneo_Tick(object sender, EventArgs e)
        {
            var tarea = new System.Threading.Thread(new System.Threading.ThreadStart(escanear));
            tarea.Start();
        }

        private void ItemAdapter_Click(object sender, EventArgs e)
        {
            var item = (MenuNetIn)sender;
            item.adaptador.toggle();
            cambiarIcono(item.adaptador.estado, item);
        }

        private void cambiarIcono(bool status, ToolStripItem menu)
        {
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

        private void toggleAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adaptadores)
            {
                item.adaptador.toggle();
            }
        }

        private void disableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adaptadores)
            {
                item.adaptador.disable();
            }
        }

        private void enableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in adaptadores)
            {
                item.adaptador.enable();
            }
        }

    }
}
