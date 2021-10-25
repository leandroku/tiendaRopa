using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TallerTiendaRopa;

namespace TiendaRopa
{
    public partial class PagPrincipal : Form
    {

        static int cantidadDePrendas;

        ArrayList Prendas = new ArrayList();

        public PagPrincipal()
        {
            InitializeComponent();

            Mostrar();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
                                 

        }
        //metodo para mostar los registros de la base de datos 
        public void Mostrar()
        {
            //conexion a la base de datos
            var conexion = new SqlConnection("Data Source=PC-SISTEMAS\\SQLEXPRESS;Initial Catalog=bdtiendaropa;Integrated Security=T" +
            "rue");            
            //consulta a realizar en la base de datos
            var consulta = "SELECT * FROM dtprenda";
            //enviar solicitud a la base de datos
            var comando = new SqlCommand(consulta, conexion);
            // abrir conexion
            conexion.Open();

            var lector = comando.ExecuteReader();
            // limpiar la DataGridView
            dgvLista.Columns.Clear();
            dgvLista.Rows.Clear();
            // copiar los tados de la base de datos a la DataGridView
            dgvLista.Columns.Add("id_prenda", "id prenda");
            dgvLista.Columns.Add("tipo", "tipo");
            dgvLista.Columns.Add("marca", "marca");
            dgvLista.Columns.Add("talla", "talla");
            dgvLista.Columns.Add("color", "color");
            dgvLista.Columns.Add("precio", "precio");

            while (lector.Read())
            {
                dgvLista.Rows.Add(
                    lector["id_prenda"], lector["tipo"], lector["Marca"], lector["Talla"], lector["Color"], lector["Precio"]
                );
            }
            //contabilizar el numero de prendas en la DataGridView
            lblNumeroPrendas.Text = "";
            int contador = dgvLista.Rows.Count - 1;
            lblNumeroPrendas.Text = lblNumeroPrendas.Text + contador;
            //cerrar conexion
            conexion.Close();
        }

        //metodo para agregar prendas en la base de datos
        public void Agregar()
        {
            var conexion = new SqlConnection("Data Source=PC-SISTEMAS\\SQLEXPRESS;Initial Catalog=bdtiendaropa;Integrated Security=T" +
            "rue");

            var tipo = cboTipo.Text;
            var marca = txtMarca.Text;
            var talla = cboTalla.Text;
            var color = txtColor.Text;
            var precio = txtPrecio.Text;

            var consulta = "INSERT INTO dtprenda (tipo, marca, talla, color, precio) VALUES('" + tipo + "','" + marca + "','" + talla + "','" + color + "','" + precio + "')";
                                    
            var comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            var cantidadDeRegistros = comando.ExecuteNonQuery();

            if (cantidadDeRegistros > 0)
            {
                MessageBox.Show("Prenda Agregada");
            }
            else
            {
                MessageBox.Show("No se Agrego la Prenda");
            }

            conexion.Close();
        }

        //metodo para modificar registros en la base de datos
        public void Modificar()
        {
            var conexion = new SqlConnection("Data Source=PC-SISTEMAS\\SQLEXPRESS;Initial Catalog=bdtiendaropa;Integrated Security=T" +
            "rue");

            var id = txtId.Text;
            var tipo = cboTipo.Text;
            var marca = txtMarca.Text;
            var talla = cboTalla.Text;
            var color = txtColor.Text;
            var precio = txtPrecio.Text;

            var consulta = "UPDATE dtprenda SET tipo = '" + tipo + "', marca = '" + marca + "', talla = '" + talla + "', color = '" + color + "', precio = '" + precio + "' WHERE id_prenda='" + id + "'";

            var comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            var cantidadDeRegistros = comando.ExecuteNonQuery();

            if (cantidadDeRegistros > 0)
            {
                MessageBox.Show("prenda actualizada");
            }
            else
            {
                MessageBox.Show("prenda no actualizada");
            }

            conexion.Close();

        }

        //metodo para eliminar prendas en la base de datos
        public void Eliminar()
        {
            var conexion = new SqlConnection("Data Source=PC-SISTEMAS\\SQLEXPRESS;Initial Catalog=bdtiendaropa;Integrated Security=T" +
            "rue");

            var id = txtId.Text;

            var consulta = "DELETE FROM dtprenda WHERE id_prenda='"+id+"'";

            var comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            var cantidadDeRegistros = comando.ExecuteNonQuery();

            if (cantidadDeRegistros > 0)
            {
                MessageBox.Show("prenda Eliminada");
            }
            else
            {
                MessageBox.Show("prenda no Eliminada");
            }

            conexion.Close();

        }

        //metodo para actualizar la DataGridView
        public void Actualizar()
        {
            dgvLista.Columns.Clear();

            Mostrar();

        }

        // metodo para limpiar los textbox
        public void limpiar()
        {
            txtId.Clear();
            txtMarca.Clear();
            txtColor.Clear();
            txtPrecio.Clear();
        }


        private void btNuevo_Click(object sender, EventArgs e)
        {

            // Validacion de informacion con el erro..

            if (cboTipo.Text == "")
            {
                errorProvider1.SetError(cboTipo, "Debe ingresar el tipo de prenda");
                cboTipo.Focus();
                return;
            }

            if (txtMarca.Text == "")
            {
                errorProvider1.SetError(txtMarca, "Debe ingresar la marca de la prenda");
                txtMarca.Focus();
                return;
            }

            if (txtColor.Text == "")
            {
                errorProvider1.SetError(txtColor, "Debe ingresar el color de la prenda");
                txtColor.Focus();
                return;
            }

            // Validacion para el campo talla, que este no sea negativo

            int talla;

            if (!Int32.TryParse(cboTalla.Text, out talla))
            {
                errorProvider1.SetError(cboTalla, "Debe ingresar numeros en el campo Talla de prenda");
                cboTalla.Focus();
                return;
            }

            // Validacion de que el valor ingresado en talla no sea negativo

            if (talla < 0)
            {
                errorProvider1.SetError(cboTalla, "Debe ingresar un numero positivo en Talla");
                cboTalla.Focus();
                return;
            }

            // Validacion para el campo precion, que este sea un numero

            decimal precio;

            if (!Decimal.TryParse(txtPrecio.Text, out precio))
            {
                errorProvider1.SetError(txtPrecio, "Debe ingresar numeros en el campo Precio");
                txtPrecio.Focus();
                return;
            }

            // Validacion de que el valor ingresado en precio no sea negativo

            if (precio < 0)
            {
                errorProvider1.SetError(txtPrecio, "Debe ingresar un numero positivo en Precio");
                txtPrecio.Focus();
                return;
            }

            Agregar();

            Actualizar();            
        
        }


        /*metodo que permite copiar los datos de la DataGridView que hayan sido
        seleccionados y pegarlos en los textbox conrrespondientes*/
        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dgvLista.CurrentRow.Cells[0].Value.ToString();
                cboTipo.Text = dgvLista.CurrentRow.Cells[1].Value.ToString();
                txtMarca.Text = dgvLista.CurrentRow.Cells[2].Value.ToString();
                cboTalla.Text = dgvLista.CurrentRow.Cells[3].Value.ToString();
                txtColor.Text = dgvLista.CurrentRow.Cells[4].Value.ToString();
                txtPrecio.Text = dgvLista.CurrentRow.Cells[5].Value.ToString();
            }
            catch
            {

            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar();
            Actualizar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            Actualizar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
