using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerTiendaRopa
{
    class Prenda
    {

        public Prenda(string tipoPrenda, string color, string marca, int talla, decimal precio)
        {
            TipoPrenda = tipoPrenda;
            Color = color;
            Marca = marca;
            Talla = talla;
            Precio = precio;

        }

        public Prenda()
        {
            TipoPrenda = null;
            Color = null;
            Marca = null;
            Talla = 0;
            Precio = 0;
        }
        public string TipoPrenda { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public int Talla { get; set; }
        public decimal Precio { get; set; }

    }
}