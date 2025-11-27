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
            Clases.CDocVentas objetoCVentas = new Clases.CDocVentas();
            //llamar el metodo y incorporar el parametro DataGridView
            objetoCVentas.mostrarVentas(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
