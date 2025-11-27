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
    public partial class FormVentas : Form
    {
        private bool ventana = false;

        public FormVentas()
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
            Clases.CVentas objetoProductos = new Clases.CVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoProductos.mostrarVentas(dgvVentas);
            
          
        }



        
        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Valida que los campos no esten vacios
            if (txtidcliente.Text == "ID VENTAS" || txtCantidad.Text == "CANTIDAD" || txtPrecioUni.Text == "PPRECIO UNITARIO" || txtTotal.Text == "TOTAL")
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CVentas objetoVentas = new Clases.CVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoVentas.guardarVentas(txtidcliente, txtidProducto, txtCantidad, txtPrecioUni, txtTotal);
            objetoVentas.mostrarVentas(dgvVentas);
            // Ejemplo de cómo llamar el método

            //restablecer los textbox
            txtIdventa.Text = "ID VENTAS";
            txtidcliente.Text = "ID CLIENTE";
            txtidProducto.Text = "ID PRODUCTO";
            txtCantidad.Text =  "CANTIDAD";
            txtPrecioUni.Text = "PPRECIO UNITARIO";
            txtTotal.Text = "TOTAL";
            txtFecha.Text = "FECHA DE LA VENTA";
            txtidcliente.Text = "ID CLIENTE";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtidcliente.Text == "ID VENTAS" || txtCantidad.Text == "CANTIDAD" || txtPrecioUni.Text == "PPRECIO UNITARIO" || txtTotal.Text == "TOTAL")
            {
                MessageBox.Show("Por favor, seleccione la venta que desea modificar.", "Venta no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //cargar los datos en la interfaz
            Clases.CVentas objetoVentas = new Clases.CVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoVentas.modificarVentas(txtIdventa, txtidcliente, txtidProducto, txtCantidad, txtPrecioUni, txtTotal);
            objetoVentas.mostrarVentas(dgvVentas);
            // Ejemplo de cómo llamar el método

            //restablecer los textbox
            txtIdventa.Text = "ID VENTAS";
            txtidcliente.Text = "ID CLIENTE";
            txtidProducto.Text = "ID PRODUCTO";
            txtCantidad.Text = "CANTIDAD";
            txtPrecioUni.Text = "PPRECIO UNITARIO";
            txtTotal.Text = "TOTAL";
            txtFecha.Text = "FECHA DE LA VENTA";
            txtidcliente.Text = "ID CLIENTE";
        }

       
        
        private void button6_Click(object sender, EventArgs e)
        {
            if (txtidcliente.Text == "ID VENTAS" || txtCantidad.Text == "CANTIDAD" || txtPrecioUni.Text == "PPRECIO UNITARIO" || txtTotal.Text == "TOTAL")
            {
                MessageBox.Show("Por favor, seleccione la venta que desea eliminar.", "Venta no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //cargar los datos en la interfaz
            Clases.CVentas objetoVentas = new Clases.CVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoVentas.eliminarVentas(txtIdventa);
            objetoVentas.mostrarVentas(dgvVentas);
            // Ejemplo de cómo llamar el método
            //restablecer los textbox
            txtIdventa.Text = "ID VENTAS"; 
            txtidcliente.Text = "ID VENTAS";
            txtidProducto.Text = "ID PRODUCTO";
            txtCantidad.Text = "CANTIDAD";
            txtPrecioUni.Text = "PPRECIO UNITARIO";
            txtTotal.Text = "TOTAL";
            txtFecha.Text = "FECHA DE LA VENTA";
            txtidcliente.Text = "ID CLIENTE";

        }

        private void dgvVentas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Este objeto sabe cómo manejar la información de usuarios
            Clases.CVentas objetoVentas = new Clases.CVentas();
            // Llama al método seleccionarclientes del objeto creado y Le pasa todos los parámetros necesarios a La tabla donde se hizo doble clic
            objetoVentas.seleccionarVentas(dgvVentas, txtIdventa, txtidcliente, txtidProducto, txtFecha, txtCantidad, txtPrecioUni, txtTotal);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DocVentas docVentas = new DocVentas();
            docVentas.Show();


        }

        private void txtidcliente_Enter(object sender, EventArgs e)
        {
            if (txtidcliente.Text == "ID CLIENTE")
            {
                txtidcliente.Text = "";
                
            }
        }

        private void txtidProducto_Enter(object sender, EventArgs e)
        {
            if (txtidProducto.Text == "ID PRODUCTO")
            {
                txtidProducto.Text = "";
            }

        }

        private void txtidcliente_Leave(object sender, EventArgs e)
        {
            if (txtidcliente.Text == "")
            {
                txtidcliente.Text = "ID CLIENTE";
            }
        }

        private void txtidProducto_Leave(object sender, EventArgs e)
        {
            if (txtidProducto.Text == "")
            {
                txtidProducto.Text = "ID PRODUCTO";
            }
        }

        private void txtFecha_Enter(object sender, EventArgs e)
        {
            if (txtFecha.Text == "FECHA DE LA VENTA")
            {
                txtFecha.Text = "";
            }
        }

        private void txtFecha_Leave(object sender, EventArgs e)
        {
            if (txtFecha.Text == "")
            {
                txtFecha.Text = "FECHA DE LA VENTA";
            }
        }

        private void txtCantidad_Enter(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "CANTIDAD")
            {
                txtCantidad.Text = "";
            }
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "CANTIDAD";
            }
        }

        private void txtPrecioUni_Enter(object sender, EventArgs e)
        {
            if (txtPrecioUni.Text == "PRECIO UNITARIO")
            {
                txtPrecioUni.Text = "";
            }
        }

        private void txtPrecioUni_Leave(object sender, EventArgs e)
        {
            if (txtPrecioUni.Text == "")
            {
                txtPrecioUni.Text = "PRECIO UNITARIO";
            }
        }

        private void txtTotal_Enter(object sender, EventArgs e)
        {
            if (txtTotal.Text == "TOTAL")
            {
                txtTotal.Text = "";
            }
        }

        private void txtTotal_Leave(object sender, EventArgs e)
        {
            if (txtTotal.Text == "")
            {
                txtTotal.Text = "TOTAL";
            }
        }

        private void Clientes_Click(object sender, EventArgs e)
        {
            FormCliente Clientes = new FormCliente();
            Clientes.Show();
            this.Hide();

        }

        private void Productos_Click(object sender, EventArgs e)
        {
            FormProductos Productos = new FormProductos(); 
            Productos.Show();
            this.Hide();
        }

        private void Ventas_Click(object sender, EventArgs e)
        {
            LimpiarControles(this);
        }

        private void Reportes_Click(object sender, EventArgs e)
        {
            FormReportes Reportes = new FormReportes();
            Reportes.Show();
            this.Hide();
        }

        private void Contenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
