using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEjemplo.Clases
{
    internal class CDocVentas
    {
        public void mostrarVentas(DataGridView tablaCVentas)
        {
//el try catch servira para ver si hay errores
            try
            {
                //limpiar el DataSource del DataGridV para eliminar datos anteriores
                tablaCVentas.DataSource = null;
                //se crea un adaptador MySQL para ejecutar la consulta 
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT \r\n    v.Codigo_Venta,\r\n    c.Nombre_Cliente,\r\n    c.Apellido_Cliente,\r\n    p.Nombre_Producto,\r\n    cat.Nombre_Categoria,\r\n    v.Cantidad,\r\n    v.Precio_Unitario,\r\n    -- Calculando el total multiplicando cantidad por precio unitario\r\n    (v.Cantidad * v.Precio_Unitario) as Total_Calculado,\r\n    v.Total as Total_Guardado,\r\n    v.Fecha_Venta\r\nFROM Ventas v\r\nINNER JOIN Clientes c ON v.Codigo_Cliente = c.Codigo_Cliente\r\nINNER JOIN Productos p ON v.Codigo_Producto = p.Codigo_Producto\r\nINNER JOIN Categoria cat ON p.Codigo_Categoria = cat.Codigo_Categoria;", CConexion.conexion());
                //DataTable servira para almacenar los resultados de la consulta
                DataTable dcv = new DataTable();
                //llena el DataTable con los datos que se  obtenidos de la consulta
                adapter.Fill(dcv);
                tablaCVentas.DataSource = dcv;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se mostaron los datos de la base de datos, error:" + ex.ToString());
            }
        }
    }
}
