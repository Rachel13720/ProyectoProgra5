using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LogicaProyecto
{
    public class UsuarioRol
    {
        //atributos
        public int IDUsuarioRol { get; set; }

        public string Rol { get; set; }

        //lista los datos de Rol de usuario en la BD
        public DataTable Listar()
        {
            DataTable R = new DataTable();

            Conexion MyCnn = new Conexion();

            R = MyCnn.DMLSelect("SPUsuarioRolListar");

            return R;
        }


    }
}
