using CrudEjemplo.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CrudEjemplo
{
    public partial class FormCliente : Form
    {

        private bool ventana = false;
        public FormCliente()
        {
            
            InitializeComponent();

        }
        public DataGridView TablaClientes
        {
            get { return dgvTotalClientes; }
        }


        private void Salirbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizarbtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void txtId_Enter(object sender, EventArgs e)
        {
            if (txtId.Text == "ID")
            {
                txtId.Text = "";

            }
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            //cargar los datos en la interfaz
            Clases.CClient objetoClientes = new Clases.CClient();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoClientes.mostrarClientes(dgvTotalClientes);
        }

        private void txtId_Leave_1(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                txtId.Text = "ID";
            }
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if (txtNombre.Text == "NOMBRE")
            {
                txtNombre.Text = "";
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                txtNombre.Text = "NOMBRE";
            }
        }

        private void txtApellido_Enter(object sender, EventArgs e)
        {
            if (txtApellido.Text == "APELLIDO")
            {
                txtApellido.Text = "";
            }
        }

        private void txtApellido_Leave(object sender, EventArgs e)
        {
            if (txtApellido.Text == "")
            {
                txtApellido.Text = "APELLIDO";
            }
        }

        private void txtmail_Enter(object sender, EventArgs e)
        {
            if (txtmail.Text == "CORREO")
            {
                txtmail.Text = "";
            }
        }

        private void txtmail_Leave(object sender, EventArgs e)
        {
            if (txtmail.Text == "")
            {
                txtmail.Text = "CORREO";
            }
        }

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            if (txtTelefono.Text == "")
            {
                txtTelefono.Text = "TELEFONO";
            }
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            if (txtTelefono.Text == "TELEFONO")
            {
                txtTelefono.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text == "NOMBRE" ||
                string.IsNullOrWhiteSpace(txtApellido.Text) || txtApellido.Text == "APELLIDO" ||
                string.IsNullOrWhiteSpace(txtmail.Text) || txtmail.Text == "CORREO" ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || txtTelefono.Text == "TELEFONO")
            {
                MessageBox.Show("Por favor, Selecione los datos del cliente que quiere eliminar.", "Datos no seleccionados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CClient objetoClientes = new Clases.CClient();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoClientes.eliminarClientes(txtId);
            objetoClientes.mostrarClientes(dgvTotalClientes);
            txtId.Text = "ID";
            txtNombre.Text = "NOMBRE";
            txtApellido.Text = "APELLIDO";
            txtmail.Text = "CORREO";
            txtTelefono.Text = "TELEFONO";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text == "NOMBRE" ||
                string.IsNullOrWhiteSpace(txtApellido.Text) || txtApellido.Text == "APELLIDO" ||
                string.IsNullOrWhiteSpace(txtmail.Text) || txtmail.Text == "CORREO" ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || txtTelefono.Text == "TELEFONO")
            {
                MessageBox.Show("Por favor, Selecione los datos del cliente que quiere modificar.", "Datos no seleccionados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CClient objetoClientes = new Clases.CClient();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoClientes.modificarClientes(txtId, txtNombre, txtApellido, txtmail, txtTelefono);
            objetoClientes.mostrarClientes(dgvTotalClientes);
            txtId.Text = "ID";
            txtNombre.Text = "NOMBRE";
            txtApellido.Text = "APELLIDO";
            txtmail.Text = "CORREO";
            txtTelefono.Text = "TELEFONO";
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text == "NOMBRE" ||
                string.IsNullOrWhiteSpace(txtApellido.Text) || txtApellido.Text == "APELLIDO" ||
                string.IsNullOrWhiteSpace(txtmail.Text) || txtmail.Text == "CORREO" ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) || txtTelefono.Text == "TELEFONO")
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //cargar los datos en la interfaz
            Clases.CClient objetoClientes = new Clases.CClient();

            //llamar el metodo y incorporar el parametro DataGridView
            objetoClientes.guardarClientes(txtNombre, txtApellido, txtmail, txtTelefono);
            objetoClientes.mostrarClientes(dgvTotalClientes);
            // Ejemplo de cómo llamar el método
            txtId.Text = "ID";
            txtNombre.Text = "NOMBRE";
            txtApellido.Text = "APELLIDO";
            txtmail.Text = "CORREO";
            txtTelefono.Text = "TELEFONO";
        }

        private void dgvTotalClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTotalClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Este objeto sabe cómo manejar la información de usuarios
            Clases.CClient objetoUsuarios = new Clases.CClient();
            // Llama al método seleccionarclientes del objeto creado y Le pasa todos los parámetros necesarios a La tabla donde se hizo doble clic
            objetoUsuarios.seleccionarClientes(dgvTotalClientes, txtId, txtNombre, txtApellido, txtmail, txtTelefono);
        }

       

        

        private void Clientes_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
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
            FormReportes llamar = new FormReportes();
            llamar.Show();
            this.Hide();
        }
    }
}
