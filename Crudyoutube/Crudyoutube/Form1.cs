using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlConnector;
using Org.BouncyCastle.Utilities;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;


namespace Crudyoutube

{
   
    public partial class Form1 : Form
        

    {
       
        public Form1()
          
       
        {
            InitializeComponent();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            MemoryStream ms = new MemoryStream();
            pbImage.Image.Save(ms, ImageFormat.Jpeg);
            byte[] aByte = ms.ToArray();

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();


            int cedula = int.Parse(txtcedula.Text);
            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;
            int celular = int.Parse(txtcelular.Text);     
            string cargo = txtcargo.Text;
            string oficina = txtoficina.Text;


                string sql = "INSERT INTO persona (cedula, nombre, apellido, celular, cargo, oficina, foto) VALUES ('" + cedula + "', '" + nombre + "', '" + apellido + "', '" + celular + "','" + cargo + "', '" + oficina + "', @foto)";


                try
                {
                    MySql.Data.MySqlClient.MySqlCommand comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexionBD);
                    comando.Parameters.AddWithValue("foto", aByte);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Guardado");
                    limpiar();
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);

                }
                finally
                {
                    conexionBD.Close();
                
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cedula = txtcedula.Text;
            MySql.Data.MySqlClient.MySqlDataReader reader = null;

            string sql = "SELECT cedula, nombre, apellido, celular, cargo, oficina, foto FROM persona WHERE cedula LIKE '" + cedula + "'LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySql.Data.MySqlClient.MySqlCommand comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MemoryStream ms = new MemoryStream((byte[])reader["foto"]);
                        Bitmap bm = new Bitmap(ms);
                        pbImage.Image = bm;
                        txtcedula.Text = reader.GetString(0);
                        txtnombre.Text = reader.GetString(1);
                        txtapellido.Text = reader.GetString(2);
                        txtcelular.Text = reader.GetString(3);
                        txtcargo.Text = reader.GetString(4);
                        txtoficina.Text = reader.GetString(5);
                        
                        
                        

                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int cedula = int.Parse(txtcedula.Text);
            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;
            int celular = int.Parse(txtcelular.Text);
            string cargo = txtcargo.Text;
            string oficina = txtoficina.Text;

            string sql = "UPDATE persona SET cedula ='" + cedula + "', nombre = '" + nombre + "', apellido =  '" + apellido + "', celular = '" + celular + "', cargo='" + cargo + "', oficina= '" + oficina + "'WHERE cedula='" + cedula + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySql.Data.MySqlClient.MySqlCommand comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");
                limpiar();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error al modificar" + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int cedula = int.Parse(txtcedula.Text);


            string sql = "DELETE FROM persona WHERE  cedula= '" + cedula + "'"; 

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySql.Data.MySqlClient.MySqlCommand comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error al Eliminar " + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtcedula.Text = "";
            txtnombre.Text = "";
            txtapellido.Text = "";
            txtcelular.Text = "";
            txtcargo.Text = "";
            txtoficina.Text = "";
            pbImage.Image = null;
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdselecionar = new OpenFileDialog();
            ofdselecionar.Filter = "foto |*.jpeg; *.jpg; *.png";
            ofdselecionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ofdselecionar.Title = "Selecionar foto";

            if(ofdselecionar.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image = Image.FromFile(ofdselecionar.FileName);
            }
        }
    }
       
 }
