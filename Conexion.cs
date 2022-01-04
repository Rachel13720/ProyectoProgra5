using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class Conexion
    {
        //contiene info de la cadena de conexión a usar por la clase.
        String CadenaDeConexion { get; set; }

        //este listado se usa para agregar los parámetros 
        //que se pasarán al procedimiento almacenado.

        public List<SqlParameter> ListadoDeParametros = new List<SqlParameter>();

        //Ejecuta un procedimiento almacenado, sirve para crear un datatable y hacer consultas
        public int DMLUpdateDeleteInsert(String NombreSP)
        {
            int Retorno = 0;

            using (SqlConnection MyCnn = new SqlConnection(CadenaDeConexion))

            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;

                if (ListadoDeParametros != null && ListadoDeParametros.Count > 0)
                {
                    foreach (SqlParameter item in ListadoDeParametros)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }

                MyCnn.Open();

                Retorno = MyComando.ExecuteNonQuery();
            }

            return Retorno;
        }


        //Ejecuta un procedimiento almacenado, sirve para crear un datatable y hacer consultas
        public DataTable DMLSelect(String NombreSP, bool CargarEsquemaDeTabla = false)
        {
            DataTable Retorno = new DataTable();

            using (SqlConnection MyCnn = new SqlConnection(CadenaDeConexion))
            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;
                if (ListadoDeParametros != null && ListadoDeParametros.Count > 0)
                {
                    foreach (SqlParameter item in ListadoDeParametros)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }
                SqlDataAdapter MyAdaptador = new SqlDataAdapter(MyComando);

                if (CargarEsquemaDeTabla)
                {
                    MyAdaptador.FillSchema(Retorno, SchemaType.Source);
                }
                else
                {
                    MyAdaptador.Fill(Retorno);
                }
            }
            return Retorno;
        }


        //Ejecuta un procedimiento almacenado, sirve para crear un datatable y hacer consultas
        public Object DMLConRetornoEscalar(String NombreSP)
        {
            Object Retorno = null;
            using (SqlConnection MyCnn = new SqlConnection(CadenaDeConexion))

            {
                SqlCommand MyComando = new SqlCommand(NombreSP, MyCnn);
                MyComando.CommandType = CommandType.StoredProcedure;

                if (ListadoDeParametros != null && ListadoDeParametros.Count > 0)
                {
                    foreach (SqlParameter item in ListadoDeParametros)
                    {
                        MyComando.Parameters.Add(item);
                    }
                }
                MyCnn.Open();
                Retorno = MyComando.ExecuteScalar();
            }

            return Retorno;
        }

        //Constructor que carga la informacion del app.config
        //en la cadena de conexion al crear una instancia nueva de la clase
        public Conexion()
        {
            this.CadenaDeConexion = ConfigurationManager.ConnectionStrings["CNNSTR"].ToString();

        }


    }

}

