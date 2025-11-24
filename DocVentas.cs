using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEjemplo
{
    public partial class DocVentas : Form
    {
        public DocVentas()
        {
            InitializeComponent();
        }

        private void DocVentas_Load(object sender, EventArgs e)
        {
            //cargar los datos en la interfaz
            Clases.CVentas objetoVentas = new Clases.CVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoVentas.mostrarVentas(dataGridView1);
        }
    }
}
