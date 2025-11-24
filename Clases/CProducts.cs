using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CrudEjemplo.Clases
{
    internal class CProducts
    {
        //crear un metodo pra mostrar los productos 
        public void mostrarProductos(DataGridView tablaProductos)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //limpiar el DataSource del DataGridV para eliminar datos anteriores
                tablaProductos.DataSource = null;
                //se crea un adaptador MySQL para ejecutar la consulta 
                MySqlDataAdapter adapter = new MySqlDataAdapter("select * from Productos;", CConexion.conexion());
                //DataTable servira para almacenar los resultados de la consulta
                DataTable dp = new DataTable();
                //llena el DataTable con los datos que se  obtenidos de la consulta
                adapter.Fill(dp);
                tablaProductos.DataSource = dp;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se mostaron los datos de la base de datos, error:" + ex.ToString());
            }
        }


        //crear un metodo pra guardar los Productos 
        public void guardarProductos(TextBox nombre, TextBox precio, TextBox existencias, ComboBox codCategoria)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para insertar datos
                string query = "insert into Productos(Nombre_Producto,Precio,Existencias,Codigo_Categoria)" +
                    "VALUES ('" + nombre.Text + "','" + precio.Text + "','" + existencias.Text + "','" + codCategoria.SelectedValue + "');";

                MySqlConnection conexion = CConexion.conexion();
                conexion.Open();
                // Ejecuta el comando en la base de datos
                MySqlCommand mycomand = new MySqlCommand(query, conexion);
                MySqlDataReader reader = mycomand.ExecuteReader();
                MessageBox.Show("Se guardaron los registros");
                //muestra el recorrido del DataGridV de la tabla
                while (reader.Read())
                {

                }
                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar" + ex.ToString());
            }
        }

        //crear un metodo selecionar
        //método tomara los datos de una fila seleccionada en una tabla 
        public void seleccionarProductos(DataGridView tablaProductos, TextBox id, TextBox nombre, TextBox precio, TextBox existencias, ComboBox codCategoria)
        {
            //el try catch servira para ver si hay errores
            try
            {
                // Toma el valor de la celda 0 (primera columna) de la fila seleccionada y lo muestra en la caja de texto del ID
                id.Text = tablaProductos.CurrentRow.Cells[0].Value.ToString();
                //y los mismo con las siguientes
                nombre.Text = tablaProductos.CurrentRow.Cells[1].Value.ToString();
                precio.Text = tablaProductos.CurrentRow.Cells[2].Value.ToString();
                existencias.Text = tablaProductos.CurrentRow.Cells[3].Value.ToString();
                codCategoria.SelectedValue = tablaProductos.CurrentRow.Cells[4].Value.ToString();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar" + ex.ToString());
            }
        }

        //crear un metodo pra modificar los productos 
        public void modificarProductos(TextBox id, TextBox nombre, TextBox precio, TextBox existencias, ComboBox codCategoria)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para modificar datos
                string query = "Update  Productos set Nombre_Producto='"
                    + nombre.Text + "',Precio='" + precio.Text + "',Existencias='" + existencias.Text + "',Codigo_Categoria='" + codCategoria.SelectedValue + "' where Codigo_Producto='" + id.Text + "';";

                MySqlConnection conexion = CConexion.conexion();
                conexion.Open();
                // Ejecuta el comando en la base de datos
                MySqlCommand mycomand = new MySqlCommand(query, conexion);
                MySqlDataReader reader = mycomand.ExecuteReader();
                MessageBox.Show("Se modifico correctamente los registros");
                //muestra el recorrido del DataGridV de la tabla
                while (reader.Read())
                {

                }
                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar" + ex.ToString());
            }
        }

        //crear un metodo pra eliminar los productos 
        public void eliminarProductos(TextBox Id)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para modificar datos
                string query = "delete from  Productos where Codigo_Producto='" + Id.Text + "';";

                MySqlConnection conexion = CConexion.conexion();
                conexion.Open();
                // Ejecuta el comando en la base de datos
                MySqlCommand mycomand = new MySqlCommand(query, conexion);
                MySqlDataReader reader = mycomand.ExecuteReader();
                MessageBox.Show("Se elimino correctamente el Producto");
                //muestra el recorrido del DataGridV de la tabla
                while (reader.Read())
                {

                }
                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar" + ex.ToString());
            }
        }

        public DataTable ObtenerCategorias(ComboBox categoria)
        {
            MySqlConnection conexion = CConexion.conexion();
            conexion.Open();
            string query = "SELECT Codigo_Categoria, Nombre_Categoria FROM categoria";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            categoria.DataSource = dt;
            categoria.DisplayMember = "Nombre_Categoria";
            categoria.ValueMember = "Codigo_Categoria";



            conexion.Close();
            return dt;
        }

        public void reporteProductos()
        {
            try
            {
                string query = "SELECT * FROM productos;";
                string ruta = "C:\\Users\\Daniel\\PDF\\";



                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                DataTable dt = new DataTable();


                using (MySqlConnection conexion = CConexion.conexion())
                {
                    conexion.Open();
                    MySqlCommand mc = new MySqlCommand(query, conexion);

                    MySqlDataAdapter da = new MySqlDataAdapter(mc);
                    da.Fill(dt);
                }


                Document doc = new Document(PageSize.A4);
                PdfWriter.GetInstance(doc, new FileStream(ruta + "ReporteProductos.pdf", FileMode.Create));
                doc.Open();
                PdfPCell titulo = new PdfPCell(new Phrase("Reporte de Productos\n\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
                titulo.VerticalAlignment = Element.ALIGN_CENTER;
                titulo.HorizontalAlignment = Element.ALIGN_CENTER;
                titulo.Border = 0;


                PdfPTable encabezado = new PdfPTable(1);

                encabezado.WidthPercentage = 100;

                string rutaImagen = "C:\\Users\\Daniel\\Desktop\\Proyecto\\HOME2\\Logo\\Cafeterito-Logo.png";
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutaImagen);
                imagen.ScaleToFit(80f, 80f);
                PdfPCell celdaImagen = new PdfPCell(imagen);
                celdaImagen.Border = 0;
                celdaImagen.HorizontalAlignment = Element.ALIGN_CENTER;
                celdaImagen.VerticalAlignment = Element.ALIGN_CENTER;
                encabezado.AddCell(celdaImagen);
                encabezado.AddCell(titulo);

                doc.Add(encabezado);
                doc.Add(new Paragraph("\n"));




                PdfPTable tabla = new PdfPTable(dt.Columns.Count);
                //agregar los encabezados de la tabla
                foreach (DataColumn columna in dt.Columns)
                {
                    tabla.AddCell(new Phrase(columna.ColumnName, FontFactory.GetFont("Arial", 12, Font.BOLD)));
                }
                //agregar los datos de la tabla
                foreach (DataRow fila in dt.Rows)
                {
                    foreach (var item in fila.ItemArray)
                    {
                        tabla.AddCell(new Phrase(item.ToString(), FontFactory.GetFont("Arial", 12, Font.NORMAL)));
                    }
                }
                doc.Add(tabla);
                doc.Close();

                // Lógica para generar y descargar el reporte de clientes
                MessageBox.Show("Reporte de Productos descargado correctamente.");
                System.Diagnostics.Process.Start(ruta + "ReporteProductos.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el reporte de Productos: " + ex.ToString());
            }
        }

    }
}
