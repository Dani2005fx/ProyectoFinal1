using Google.Protobuf.WellKnownTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEjemplo.Clases
{
    internal class CVentas
    {
        //crear un metodo pra mostrar los Ventas 
        public void mostrarVentas(DataGridView tablaVentas)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //limpiar el DataSource del DataGridV para eliminar datos anteriores
                tablaVentas.DataSource = null;
                //se crea un adaptador MySQL para ejecutar la consulta 
                MySqlDataAdapter adapter = new MySqlDataAdapter("select * from Ventas", CConexion.conexion());
                //DataTable servira para almacenar los resultados de la consulta
                DataTable dv = new DataTable();
                //llena el DataTable con los datos que se  obtenidos de la consulta
                adapter.Fill(dv);
                tablaVentas.DataSource = dv;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se mostaron los datos de la base de datos, error:" + ex.ToString());
            }
        }


        //crear un metodo pra guardar los Ventas 
        public void guardarVentas(TextBox idClient, TextBox idProdut, TextBox cantidad, TextBox precioUnit, TextBox total)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para insertar datos
                string query = "INSERT INTO Ventas (Codigo_Cliente,Codigo_Producto,Cantidad,Precio_Unitario,Total)" +
                    "VALUES ('" + idClient.Text + "','" + idProdut.Text + "','" + cantidad.Text + "','" + precioUnit.Text + "','" + total.Text + "');";

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
        public void seleccionarVentas(DataGridView tablaVentas, TextBox idVenta, TextBox idClient, TextBox idProdut, TextBox fechaventa, TextBox cantidad, TextBox precioUnit, TextBox total)
        {
            //el try catch servira para ver si hay errores
            try
            {
                // Toma el valor de la celda 0 (primera columna) de la fila seleccionada y lo muestra en la caja de texto del ID
                idVenta.Text = tablaVentas.CurrentRow.Cells[0].Value.ToString();
                //y los mismo con las siguientes
                idClient.Text = tablaVentas.CurrentRow.Cells[1].Value.ToString();
                idProdut.Text = tablaVentas.CurrentRow.Cells[2].Value.ToString();
                fechaventa.Text = tablaVentas.CurrentRow.Cells[3].Value.ToString();
                cantidad.Text = tablaVentas.CurrentRow.Cells[4].Value.ToString();
                precioUnit.Text = tablaVentas.CurrentRow.Cells[5].Value.ToString();
                total.Text = tablaVentas.CurrentRow.Cells[6].Value.ToString();
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar" + ex.ToString());
            }
        }

        //crear un metodo pra modificar los ventas 
        public void modificarVentas(TextBox idVenta, TextBox idClient, TextBox idProdut, TextBox cantidad, TextBox precioUnit, TextBox total)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para modificar datos
                string query = "Update  Ventas set Codigo_Cliente='"
                    + idClient.Text + "',Codigo_Producto='" + idProdut.Text + "',Cantidad='" + cantidad.Text + "',Precio_Unitario='" + precioUnit.Text + "',Total='" + total.Text + "' where Codigo_Venta='" + idVenta.Text + "';";

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

        //crear un metodo pra eliminar los Ventas 
        public void eliminarVentas(TextBox idVenta)
        {
            //el try catch servira para ver si hay errores
            try
            {
                //comando sql para modificar datos
                string query = "delete from  Ventas where Codigo_Venta='" + idVenta.Text + "';";

                MySqlConnection conexion = CConexion.conexion();
                conexion.Open();
                // Ejecuta el comando en la base de datos
                MySqlCommand mycomand = new MySqlCommand(query, conexion);
                MySqlDataReader reader = mycomand.ExecuteReader();
                MessageBox.Show("Se elimino correctamente el Ventas");
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

        public void reporteVentas()
        {
            try
            {
                string query = "SELECT * FROM Ventas;";
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
                PdfWriter.GetInstance(doc, new FileStream(ruta + "ReporteVentas.pdf", FileMode.Create));
                doc.Open();
                PdfPCell titulo = new PdfPCell(new Phrase("Reporte de Ventas\n\n", FontFactory.GetFont("Arial", 16, Font.BOLD)));
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
                MessageBox.Show("Reporte de Ventas descargado correctamente.");
                System.Diagnostics.Process.Start(ruta + "ReporteVentas.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el reporte de Ventas: " + ex.ToString());
            }
        }

    }
}
    

