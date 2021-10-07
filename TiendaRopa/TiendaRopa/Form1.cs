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
    public partial class Form1 : Form
    {

        static int numeroPredas;
        ArrayList Prendas = new ArrayList();

        public Form1()
        {
            InitializeComponent();  

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvLista.DataSource = Prendas;
        }

        private void btNuevo_Click(object sender, EventArgs e)
        {
            // TODO: Validacion de informacion con el erro..

            if (cboTipo.Text == "")
            {
                errorProvider1.SetError(cboTipo, "Debe ingresar la identificacion del usuario");
                cboTipo.Focus();
                return;
            }


            // TODO:Validacion para el campo precion, que este no sea negativo
            
            decimal precio;
            if (!Decimal.TryParse(txtPrecio.Text, out precio))
            {
                errorProvider1.SetError(txtPrecio, "Debe ingresar numeros en el campo Precio");
                txtPrecio.Focus();
                return;
            }

            // Validacion de que el valor ingresado en precio no sea negativo, pues ninguna 
            // prenda puede tener un precio negativo.

            if (precio < 0)
            {
                errorProvider1.SetError(txtPrecio, "Debe ingresar un numero positivo en salario    ");
                txtPrecio.Focus();
                return;
            }

            //////////////////////////////////////////////////////////

            // Agregacion de la nueva prenda al ArrayList

            //Prenda casas = new Prenda("casas", "casas", "casas", 12, 12222);
            //Prendas.Add(casas);

            Prenda nuevaPrenda = new Prenda(cboTipo.Text, txtColor.Text, txtMarca.Text, Convert.ToInt32(cboTalla.Text), precio);
            Prendas.Add(nuevaPrenda);

            dgvLista.DataSource = null;
            dgvLista.DataSource = Prendas;



            // TODO: limpieza de campos


            // Actualizacion del campop numero de prendas
            
            numeroPredas = Prendas.Count;
            lblNumeroPrendas.Text = Convert.ToString(numeroPredas);
        }

        
    }
}
