using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class ProductoCategoria
    {
        //atributos

        public int IDProductoCategoria { get; set; }

        public string Categoria { get; set; }

        //metodos

         //Lista los datos de la categoria del producto en la BD 
        public DataTable Listar()
        {
            DataTable R = new DataTable();

            Conexion MyCnn = new Conexion();

            R = MyCnn.DMLSelect("SPProductoCategoriaListar");

            return R;
        }
    }
}
