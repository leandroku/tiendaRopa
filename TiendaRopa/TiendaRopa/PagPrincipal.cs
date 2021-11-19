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
            //dgvLista.DataSource = Prendas;                      

        }

        public void Mostrar()
        {
            //conexion a la base de datos
            var conexion = new SqlConnection(Properties.Settings.Default.cn);
            // comando para visualizar la tabla dtprenda
            var consulta = "SELECT * FROM dtprenda";

            var comando = new SqlCommand(consulta, conexion);

            conexion.Open();

            var lector = comando.ExecuteReader();

            dgvLista.Columns.Clear();
            dgvLista.Rows.Clear();

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

            lblNumeroPrendas.Text = "";
            int contador2 = dgvLista.Rows.Count - 1;
            lblNumeroPrendas.Text = lblNumeroPrendas.Text + contador2;

            conexion.Close();
        }

        public void Agregar()
        {
            //conexion a la base de datos
            var conexion = new SqlConnection(Properties.Settings.Default.cn);

            //tomando los datos de las cajas de texto y colocandolas en variables
            var tipo = cboTipo.Text;
            var marca = txtMarca.Text;
            var talla = cboTalla.Text;
            var color = txtColor.Text;
            var precio = txtPrecio.Text;

            // metodo para conectar con el procedimiento almacenado
            var cmd = new SqlCommand("SP_AGREGAR", conexion);

            //almacenando los datos en las variables del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TIPO", tipo));
            cmd.Parameters.Add(new SqlParameter("@MARCA", marca));
            cmd.Parameters.Add(new SqlParameter("@TALLA", talla));
            cmd.Parameters.Add(new SqlParameter("@COLOR", color));
            cmd.Parameters.Add(new SqlParameter("@PRECIO", precio));

            // Abrir la conexion
            conexion.Open();

            // verificar si se realizaron los cambios 
            var cantidadDeRegistros = cmd.ExecuteNonQuery();
           
            if (cantidadDeRegistros > 0)
            {
                MessageBox.Show("Prenda creada");
            }
            else
            {
                MessageBox.Show("No se creó la Prenda");
            }

            conexion.Close();
        }

        public void Modificar()
        {
            //conexion a la base de datos
            var conexion = new SqlConnection(Properties.Settings.Default.cn);

            //tomando los datos de las cajas de texto y colocandolas en variables
            var id = txtId.Text;
            var tipo = cboTipo.Text;
            var marca = txtMarca.Text;
            var talla = cboTalla.Text;
            var color = txtColor.Text;
            var precio = txtPrecio.Text;

            // metodo para conectar con el procedimiento almacenado
            var cmd = new SqlCommand("SP_ACTUALIZAR", conexion);

            //almacenando los datos en las variables del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID_PRENDA", id));
            cmd.Parameters.Add(new SqlParameter("@TIPO", tipo));
            cmd.Parameters.Add(new SqlParameter("@MARCA", marca));
            cmd.Parameters.Add(new SqlParameter("@TALLA", talla));
            cmd.Parameters.Add(new SqlParameter("@COLOR", color));
            cmd.Parameters.Add(new SqlParameter("@PRECIO", precio));

            // Abrir la conexion
            conexion.Open();

            // verificar si se realizaron los cambios 
            var cantidadDeRegistros = cmd.ExecuteNonQuery();

            if (cantidadDeRegistros > 0)
            {
                MessageBox.Show("Prenda Modificada");
            }
            else
            {
                MessageBox.Show("No se pudo modificar la Prenda");
            }

            conexion.Close();

        }

        public void Eliminar()
        {
            //conexion a la base de datos
            var conexion = new SqlConnection(Properties.Settings.Default.cn);

            //tomando el datos de las cajas id y colocandola en variable
            var id = txtId.Text;

            // metodo para conectar con el procedimiento almacenado
            var cmd = new SqlCommand("SP_ELIMINAR", conexion);

            //almacenando el dato en la variables ID_PRENDA del procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID_PRENDA", id));

            // Abrir la conexion
            conexion.Open();

            // verificar si se realizaron los cambios 
            var cantidadDeRegistros = cmd.ExecuteNonQuery();

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

        public void Actualizar()
        {
            dgvLista.Columns.Clear();

            Mostrar();

        }

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
