using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class Cliente
    {
        //variables globales
        public int IDCliente { get; set; }

        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public string Comentario { get; set; }

        public bool Activo { get; set; }

        public Cliente()
        {
            Activo = true;
        }

        //agrega los datos del clientes en la BD
        public bool Agregar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPClienteAgregar");

                if (retorno > 0)
                {
                    R = true;
                }


            }
            catch (Exception)
            {

                throw;
            }


            return R;
        }

        //Edita los datos del cliente en la BD
        public bool Editar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdCliente", this.IDCliente));

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPClienteEditar");

                if (retorno > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }


            return R;
        }

        //Desactiva los datos del cliente en la BD
        public bool Desactivar()
        {
            bool R = false;
            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdCliente", this.IDCliente));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPClienteDesactivar");

                if (retorno > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return R;
        }

        //Activa los datos del cliente en la BD
        public bool Activar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdCliente", this.IDCliente));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPClienteActivar");

                if (retorno > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return R;
        }

        //Consulta los datos del cliente por el ID, en la BD
        public bool ConsultarPorID()
        {
            bool R = false;

            try
            {
                Conexion MiConexion = new Conexion();

                MiConexion.ListadoDeParametros.Add(new SqlParameter("@IdCliente", this.IDCliente));

                DataTable retorno = MiConexion.DMLSelect("SPClienteConsultarPorID");

                if (retorno.Rows.Count > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return R;
        }

        //Consula los datos del cliente por la cedula, en la BD
        public bool ConsultarPorCedula()
        {
            bool R = false;
            try
            {
                Conexion MiConexion = new Conexion();

                MiConexion.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));

                DataTable retorno = MiConexion.DMLSelect("SPClienteConsultarPorCedula");

                if (retorno.Rows.Count > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return R;
        }

        //Consula los datos del cliente por el email, en la BD
        public bool ConsultarPorEmail()
        {
            bool R = false;

            try
            {
                Conexion ObjConexion = new Conexion();

                ObjConexion.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

                DataTable result = ObjConexion.DMLSelect("SPClienteConsultarPorEmail");

                if (result.Rows.Count > 0)
                {
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }


            return R;
        }

        //Lista los datos del cliente en la BD, por filtro y activos
        public DataTable Listar(bool VerActivos = true, string Filtro = "")
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@VerActivos", VerActivos));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Filtro", Filtro));

            R = MiCnn.DMLSelect("SPClientesListar");

            return R;
        }

        ////Consula los datos del cliente en la BD
        public Cliente Consultar(int pIDCliente)
        {
            Cliente R = new Cliente();

            Conexion MyCnn = new Conexion();

            MyCnn.ListadoDeParametros.Add(new SqlParameter("@IdCliente", pIDCliente));

            DataTable DatosCliente = new DataTable();
            DatosCliente = MyCnn.DMLSelect("SPClienteConsultar");

            //Valida los datos del cliente
            if (DatosCliente.Rows.Count > 0)
            {

                DataRow MiFila = DatosCliente.Rows[0];

                R.IDCliente = Convert.ToInt32(MiFila["IDCliente"]);
                R.Cedula = Convert.ToString(MiFila["Cedula"]);
                R.Nombre = Convert.ToString(MiFila["Nombre"]);
                R.Telefono = Convert.ToString(MiFila["Telefono"]);
                R.Direccion = Convert.ToString(MiFila["Direccion"]);
                R.Email = Convert.ToString(MiFila["Email"]);
                R.Activo = Convert.ToBoolean(MiFila["Activo"]);

            }

            return R;
        }

    }
}
