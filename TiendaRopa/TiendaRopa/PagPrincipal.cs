using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvLista.DataSource = Prendas;
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


            Prenda nuevaPrenda = new Prenda(cboTipo.Text, txtColor.Text, txtMarca.Text, talla, precio);
            
            Prendas.Add(nuevaPrenda);

            dgvLista.DataSource = null;
            dgvLista.DataSource = Prendas;


            // TODO: limpieza de campos



            // Actualizacion del campop numero de prendas
            
            cantidadDePrendas = Prendas.Count;
            lblNumeroPrendas.Text = Convert.ToString(cantidadDePrendas);
        
        }

        

    }
}
