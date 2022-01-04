using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class VentaDetalle
    {
        //atributos
        public decimal CantidadVendida { get; set; }
        public decimal PrecioVenta { get; set; }
        public Producto MiProducto { get; set; }

        //Constructor
        public VentaDetalle()
        {
            MiProducto = new Producto();
        }
    }
}
