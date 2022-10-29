using System;

using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlConnector;

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
            int cedula = int.Parse(txtcedula.Text);
            string nombre = txtnombre.Text;
            string apellido = txtapellido.Text;
            int celular = int.Parse(txtcelular.Text);     
            string cargo = txtcargo.Text;
            string oficina = txtoficina.Text;

            string sql = "INSERT INTO persona (cedula, nombre, apellido, celular, cargo, oficina) VALUES ('" + cedula + "', '" + nombre + "', '" + apellido + "', '" + celular + "','" + cargo + "', '" + oficina + "')";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySql.Data.MySqlClient.MySqlCommand comando = new MySql.Data.MySqlClient.MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro guardado");
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

            string sql = "SELECT cedula, nombre, apellido, celular, cargo, oficina FROM persona WHERE cedula LIKE '" + cedula + "'LIMIT 1";
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
                MessageBox.Show("Registro Eliminadoo");
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
        }
    }
       
 }
