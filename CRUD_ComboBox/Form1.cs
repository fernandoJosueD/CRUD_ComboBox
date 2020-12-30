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

namespace CRUD_ComboBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cargarCategorias();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            //identificar que selecciona el usuario en el combobox selectedValue trae el valor del id que selecciono
            int idCategoria = int.Parse(cbxCategoria.SelectedValue.ToString());

            try
            {

            
            String codigo = txtCodigo.Text;
            String nombre = txtNombre.Text;
            String descripcion = txtDescripcion.Text;
            Double precio_publico = Double.Parse(txtPrecioPublico.Text);
            int existencias = int.Parse(txtExistencias.Text);

                if (codigo!=""&& nombre!=""&& descripcion!="" && precio_publico> 0 && existencias>0)
                {
            String sql = "INSERT INTO productos (codigo, nombre, descripcion, precio_publico, existencias, idCategoria)" +
                "VALUES ('"+codigo+ "','" + nombre + "','" + descripcion + "','" + precio_publico + "','" + existencias + "','"+idCategoria+"')";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("registro Guardado");
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al guardar archivo" + ex.Message);
            }

                finally
                {
                    conexionBD.Close();
                }

            } else {
                    MessageBox.Show("debe completar todos los campos");
                   }
            } 

            catch (FormatException fex)
            {
                MessageBox.Show("datos incorrectos: " + fex.Message);
            }

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            String codigo = txtCodigo.Text;

            MySqlDataReader reader = null;

            String sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias, idCategoria FROM productos WHERE codigo LIKE '"+ codigo +"' LIMIT 1";
                
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtCodigo.Text = reader.GetString(1);
                        txtNombre.Text = reader.GetString(2);
                        txtDescripcion.Text = reader.GetString(3);
                        txtPrecioPublico.Text = reader.GetString(4);
                        txtExistencias.Text = reader.GetString(5);
                        cbxCategoria.SelectedValue = reader.GetString(6);// trae el producto para visualizar combobox
                    }                                                      
                }else
                {
                    MessageBox.Show("no se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("error al buscar: "+ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            //identificar que selecciona el usuario en el combobox selectedValue trae el valor del id que selecciono
            int idCategoria = int.Parse(cbxCategoria.SelectedValue.ToString());

            String id = txtId.Text;
            String codigo = txtCodigo.Text;
            String nombre = txtNombre.Text;
            String descripcion = txtDescripcion.Text;
            Double precio_publico = Double.Parse(txtPrecioPublico.Text);
            int existencias = int.Parse(txtExistencias.Text);

            String sql = "UPDATE productos SET " +
                "codigo='"+codigo+"'," +
                "nombre='" + nombre + "'," +
                "descripcion='" + descripcion + "'," +
                "precio_publico='" + precio_publico + "'," +
                "existencias='" + existencias + "' ," +
                " idCategoria='"+idCategoria +"' WHERE id='"+id+"'" ;

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                limpiar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("error al Modificar archivo" + ex.Message);

            }

            finally
            {
                conexionBD.Close();
            }

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            String id = txtId.Text;

            String sql = "DELET FROM productos WHERE id='" + id + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error al Eliminar archivo" + ex.Message);
            }

            finally
            {
                conexionBD.Close();
            }


        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";


        }


        private void cargarCategorias()
        {
            cbxCategoria.DataSource = null;
            cbxCategoria.Items.Clear();
            string sql = "SELECT id, nombre FROM categorias ORDER BY nombre ASC";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {

                //llenar tabla en combobox
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);

                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxCategoria.ValueMember = "id";
                cbxCategoria.DisplayMember = "nombre";
                cbxCategoria.DataSource = dt;

            }catch(MySqlException ex)
            {
                MessageBox.Show("error al cargar: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }



        }
        


    }

}
