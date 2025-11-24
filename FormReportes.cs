using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using CrudEjemplo.Clases;

namespace CrudEjemplo
{
    public partial class FormReportes : Form
    {
        private bool ventana = false;

        public FormReportes()
        {
            InitializeComponent();
        }



        private void Salirbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Maximizarbtn_Click(object sender, EventArgs e)
        {
            ventana = !ventana;
            if (ventana)
            {
                this.MaximumSize = Screen.FromControl(this).WorkingArea.Size;

                this.WindowState = FormWindowState.Maximized;
                Maximizarbtn.Image = CrudEjemplo.Properties.Resources.res;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Maximizarbtn.Image = CrudEjemplo.Properties.Resources.maxi;
            }


        }

        private void Minimizarbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
            Maximizarbtn.Image = CrudEjemplo.Properties.Resources.maxi;
        }

        private void Clientes_Click(object sender, EventArgs e)
        {
            FormCliente llamar = new FormCliente();
            llamar.Show();
            this.Hide();
        }

        private void Productos_Click(object sender, EventArgs e)
        {
            FormProductos llamar = new FormProductos();
            llamar.Show();
            this.Hide();
        }

        private void Ventas_Click(object sender, EventArgs e)
        {
            FormVentas llamar = new FormVentas();
            llamar.Show();
            this.Hide();
        }

        private void Reportes_Click(object sender, EventArgs e)
        {

        }

        private void DescargarCliente_Click(object sender, EventArgs e)
        {
            CClient cliente = new CClient();
            cliente.reporteClientes();
        }

        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CProducts producto = new CProducts();
            producto.reporteProductos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CVentas ventas = new CVentas();
            ventas.reporteVentas();
        }
    }
}
