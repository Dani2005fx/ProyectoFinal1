using CrudEjemplo.Clases;
using MySql.Data.MySqlClient;
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
    public partial class InicioSecion: Form
    {
        private bool showContra = false;        
        public InicioSecion()
        {
            InitializeComponent();
        }

        //Aqui llamo la clase conexion 
        MySqlConnection conexionDB = CConexion.conexion();




        private void Form1_Load(object sender, EventArgs e)

        {
        
         
        }

        //Boton de cerrar ventana 
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //El usuario ingresa al textbox el placeholder desaparece
        private void textuser_Enter(object sender, EventArgs e)
        {
            if (textuser.Text == "USUARIO")
            {
                textuser.Text = "";
                
            }
        }

        //El usuario no ingresa nada en el texbox el placeholder aparece
        private void textuser_Leave(object sender, EventArgs e)
        {
            if(textuser.Text == "")
            {
                textuser.Text = "USUARIO";
            }
        }

        //El usuario ingresa al textbox el placeholder desaparece
        private void textcontra_Enter(object sender, EventArgs e)
        {
            if(textcontra.Text == "CONTRASEÑA")
            {
                textcontra.Text = "";           
                textcontra.UseSystemPasswordChar = true;
            }
        }

        //El usuario no ingresa nada en el texbox el placeholder aparece
        private void textcontra_Leave(object sender, EventArgs e)
        {
            if (textcontra.Text == "")
            {
                textcontra.Text = "CONTRASEÑA";
                textcontra.UseSystemPasswordChar = false;
            }
        }


        //Boton de minimizar ventana 
        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Boton de ver y ocultar contraseña 
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            showContra = !showContra;
            if (showContra)
            {
                textcontra.UseSystemPasswordChar = false;
                btnContra.Image = CrudEjemplo.Properties.Resources.Ver;
            }
            else
            {
                textcontra.UseSystemPasswordChar = true;
                btnContra.Image = CrudEjemplo.Properties.Resources.Ocultar;
            }
        }


        //Boton de acceder
        private void btnAcceder_Click(object sender, EventArgs e)
        { 

            try
            {
                //Se abre la conexion
                conexionDB.Open();

                //Aqui llama la tabla usuario para usar como parametros los datos de la tabla
                //Si tu tenes una tabla usuario diferente solo adactalo a como lo tengas
                string query = "SELECT COUNT(*) FROM usuario WHERE nombre = @user AND password = @pass";


                MySqlCommand codigo = new MySqlCommand(query, conexionDB);

                //Aqui se evalue si los datos del textbox son iguales a los de la tebla 
                codigo.Parameters.AddWithValue("@user", textuser.Text);

                codigo.Parameters.AddWithValue("@pass", textcontra.Text);

                //Aqui se para saber el valor de la primera columna de la primra fila 
                int count = Convert.ToInt32(codigo.ExecuteScalar());

                //Si el usuario no ingresa un dato  muestre un aviso
                if (textuser.Text == "USUARIO" && textcontra.Text == "CONTRASEÑA")
                {
                    labelAviso.Text = "Ingrese su usuario y contraseña porfavor. ";
                    labelAviso.ForeColor = Color.White;

                }
                else if (textuser.Text == "USUARIO")
                {
                    labelAviso.Text = "Porfavor ingrese su usuario.";
                    labelAviso.ForeColor = Color.White;
                    textuser.Focus();
                }
                else if (textcontra.Text == "CONTRASEÑA")
                {
                    labelAviso.Text = "Porfavor ingrese su contraseña.";
                    textcontra.Focus();
                    labelAviso.ForeColor = Color.White;
                }
                //aqui se comprueba si estamos en la fila correcta y nos enviara al siguiente form
                else if (count > 0)
                {
                    FormCliente llamar = new FormCliente();
                    llamar.Show();
                    this.Hide();
                }
                //Si no los datos estan incorrectos 
                //No se como poner para que muestre si uno esta incorrecto y el otro correcto 
                //Si voz encontras una manera ponla xd
                else
                {
                    labelAviso.Text = "El usuario o la contraseña son incorrectas";
                    labelAviso.ForeColor = Color.White;
                    textuser.Text = "USUARIO";
                    textcontra.Text = "CONTRASEÑA";
                    textcontra.UseSystemPasswordChar = false;
                }
            }
            catch (Exception ex)
            {
                //mensaje si la conexion esta incorrecta
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            finally
            {
                //aqui se cierra la conexion 
                if (conexionDB.State == ConnectionState.Open)
                {
                    conexionDB.Close();
                }
            }
        }


    }
}
