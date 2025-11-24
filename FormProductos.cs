using System;
using CrudEjemplo.Clases;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEjemplo
{
    public partial class FormProductos : Form
    {
        private bool ventana = false;

        public FormProductos()
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

        private void LimpiarControles(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox) ((TextBox)c).Clear();
            }
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            //cargar los datos en la interfaz
            Clases.CProducts objetoProductos = new Clases.CProducts();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoProductos.mostrarProductos(dgvProductos);
            DataTable dt = objetoProductos.ObtenerCategorias(comboBox1);
          
        }



        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Text = "NOMBRE";
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text == "NOMBRE")
            {
                txtNombre.Text = "";
            }
        }

        private void txtPrecio_Enter(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "PRECIO")
            {
                txtPrecio.Text = "";
            }
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text == "")
            {
                txtPrecio.Text = "PRECIO";
            }
        }

        private void txtstock_Enter(object sender, EventArgs e)
        {
            if (txtstock.Text == "EXISTENCIA")
            {
                txtstock.Text = "";
            }
        }

        private void txtstock_Leave(object sender, EventArgs e)
        {
            if (txtstock.Text == "")
            {
                txtstock.Text = "EXISTENCIA";
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Valida que los campos no esten vacios
            if (txtNombre.Text == "NOMBRE" || txtPrecio.Text == "PRECIO" || txtstock.Text == "EXISTENCIA" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar un producto.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CProducts objetoProductos = new Clases.CProducts();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoProductos.guardarProductos(txtNombre, txtPrecio, txtstock, comboBox1);
            objetoProductos.mostrarProductos(dgvProductos);

            //restablecer los textbox
            txtId.Text = "ID";
            txtNombre.Text = "NOMBRE";
            txtPrecio.Text = "PRECIO";
            txtstock.Text = "EXISTENCIA";
            comboBox1.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "NOMBRE" || txtPrecio.Text == "PRECIO" || txtstock.Text == "EXISTENCIA" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, Selecione los datos del producto que quiere modificar.", "Datos no seleccionados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //cargar los datos en la interfaz
            Clases.CProducts objetoProductos = new Clases.CProducts();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoProductos.modificarProductos(txtId, txtNombre, txtPrecio, txtstock, comboBox1);
            objetoProductos.mostrarProductos(dgvProductos);
            //restablecer los textbox
            txtId.Text = "ID";
            comboBox1.SelectedIndex = -1;    
            txtNombre.Text = "NOMBRE";
            txtstock.Text = "EXISTENCIA";
            txtPrecio.Text = "PRECIO";
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este objeto sabe cómo manejar la información de usuarios
            Clases.CProducts objetoProductos = new Clases.CProducts();
            // Llama al método seleccionarclientes del objeto creado y Le pasa todos los parámetros necesarios a La tabla donde se hizo doble clic
            objetoProductos.seleccionarProductos(dgvProductos, txtId, txtNombre, txtPrecio, txtstock, comboBox1);
            objetoProductos.mostrarProductos(dgvProductos);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Valida que los campos no estén vacíos  
            if (txtNombre.Text == "NOMBRE" || txtPrecio.Text == "PRECIO" || txtstock.Text == "EXISTENCIA" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, Selecione los datos del producto que quiere eliminar.", "Datos no seleccionados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CProducts objetoProductos = new Clases.CProducts();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoProductos.eliminarProductos(txtId);
            objetoProductos.mostrarProductos(dgvProductos);
            //restablecer los textbox
            txtId.Text = "ID";
            comboBox1.SelectedIndex = -1; 
            txtPrecio.Text = "PRECIO";
            txtNombre.Text = "NOMBRE";
            txtstock.Text = "EXISTENCIA";
        }

        private void Clientes_Click(object sender, EventArgs e)
        {
            FormCliente cliente = new FormCliente();
            cliente.Show();
            this.Hide();
        }

        private void Productos_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
        }

        private void Ventas_Click(object sender, EventArgs e)
        {
            FormVentas ventas = new FormVentas();
            ventas.Show();
            this.Hide();
        }

        private void Reportes_Click(object sender, EventArgs e)
        {
            FormReportes reportes = new FormReportes();
            reportes.Show();
            this.Hide();
        }
    }
}
