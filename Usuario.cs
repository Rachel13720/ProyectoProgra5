using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class Usuario
    {
        //atributos
        public int IDUsuario { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasennia { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

        public UsuarioRol Rol { get; set; }

        //constructor
        public Usuario()
        {
            Rol = new UsuarioRol();
            Activo = true;
        }

        //agrega los datos del usuario en la BD
        public bool Agregar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                Crypto MiEncriptador = new Crypto();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

                string MiPasswordEncriptado = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Pass", MiPasswordEncriptado));

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdRol", this.Rol.IDUsuarioRol));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioAgregar");

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

        //Edita los datos del usuario en la BD
        public bool Editar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Id", this.IDUsuario));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Nombre", this.Nombre));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

                Crypto MiEncriptador = new Crypto();
                string PasswordEncriptado = "";

                if (!string.IsNullOrEmpty(this.Contrasennia))
                {
                    PasswordEncriptado = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);
                }

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Pass", PasswordEncriptado));

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdRol", this.Rol.IDUsuarioRol));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioEditar");

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

        //Desactiva los datos del usuario en la BD
        public bool Desactivar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Id", this.IDUsuario));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioDesactivar");

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


        //Activa los datos del usuario en la BD
        public bool Activar()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Id", this.IDUsuario));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioActivar");

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

        //Consulta los datos del usuario en la BD
        public Usuario Consultar(int pIDUsuario)
        {
            Usuario R = new Usuario();

            Conexion MyCnn = new Conexion();

            MyCnn.ListadoDeParametros.Add(new SqlParameter("@IdUsuario", pIDUsuario));

            DataTable DatosUsuario = new DataTable();
            DatosUsuario = MyCnn.DMLSelect("SPUsuarioConsultar");

            if (DatosUsuario.Rows.Count > 0)
            {

                DataRow MiFila = DatosUsuario.Rows[0];

                R.IDUsuario = Convert.ToInt32(MiFila["IDUsuario"]);
                R.Cedula = Convert.ToString(MiFila["Cedula"]);
                R.Nombre = Convert.ToString(MiFila["Nombre"]);
                R.Telefono = Convert.ToString(MiFila["Telefono"]);
                R.Direccion = Convert.ToString(MiFila["Direccion"]);
                R.Email = Convert.ToString(MiFila["Email"]);
                R.Contrasennia = Convert.ToString(MiFila["Contrasennia"]);
                R.Rol.IDUsuarioRol = Convert.ToInt32(MiFila["IDUsuarioRol"]);
                R.Rol.Rol = Convert.ToString(MiFila["Rol"]);
                R.Activo = Convert.ToBoolean(MiFila["Activo"]);

            }

            return R;
        }

        //Consulta los datos del usuario en la BD, por ID
        public bool ConsultarPorID()
        {
            bool R = false;

            try
            {
                Conexion MiConexion = new Conexion();

                MiConexion.ListadoDeParametros.Add(new SqlParameter("@Id", this.IDUsuario));

                DataTable retorno = MiConexion.DMLSelect("SPUsuarioConsultarPorID");

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

        //Consulta los datos del usuario por cedula, en la BD
        public bool ConsultarPorCedula()
        {
            bool R = false;

            try
            {
                Conexion MiConexion = new Conexion();

                MiConexion.ListadoDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));

                DataTable retorno = MiConexion.DMLSelect("SPUsuarioConsultarPorCedula");

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

        //Consulta los datos del usuario por email, en la BD
        public bool ConsultarPorEmail()
        {
            bool R = false;

            try
            {
                Conexion ObjConexion = new Conexion();

                ObjConexion.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

                DataTable result = ObjConexion.DMLSelect("SPUsuarioConsultarPorEmail");

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

        //lista todos los datos del usuario en la BD
        public DataTable ListarTodos()
        {
            DataTable R = new DataTable();

            //paso 1.3.1 y 1.3.2
            Conexion MiConexion = new Conexion();

            //1.3.3
            R = MiConexion.DMLSelect("SPUsuariosListarTodos");

            //1.3.4
            return R;
        }

        //lista los datos del usuario por filtro, en la BD
        public DataTable Listar(bool VerActivos = true, string Filtro = "")
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@VerActivos", VerActivos));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Filtro", Filtro));

            R = MiCnn.DMLSelect("SPUsuariosListar");

            return R;
        }

        //Valida el login
        public int ValidarLogin(string pUsuario, string pPass)
        {
            int R = 0;

            this.Email = pUsuario;
            this.Contrasennia = pPass;

            Crypto MiEncriptador = new Crypto();

            string PasswordEncriptado = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@User", this.Email));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Pass", PasswordEncriptado));

            DataTable Respuesta = MiCnn.DMLSelect("SPUsuarioValidarLogin");

            if (Respuesta != null && Respuesta.Rows.Count > 0)
            {
                DataRow MiFila = Respuesta.Rows[0];

                R = Convert.ToInt32(MiFila["IDUsuario"]);
            }

            return R;
        }

        //Guarda el codigo de la recuperacion de la contraseña
        public bool GuardarCodigoRecuperacionContrasennia(string CodigoRecuperacion)
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));
                MiCnn.ListadoDeParametros.Add(new SqlParameter("@CodigoRecuperacion", CodigoRecuperacion));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioGuardarCodigoRecuperacion");

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

        //Valida el codigo de verificacion para la recuperaacion de la contraseña
        public bool ValidarCodigoVerificacion(string pUsuario, string pCodigoVerificacion)
        {
            bool R = false;

            this.Email = pUsuario;

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@User", this.Email));

            DataTable Respuesta = MiCnn.DMLSelect("SPUsuarioObtenerCodigoRecuperacion");

            if (Respuesta != null && Respuesta.Rows.Count > 0)
            {
                DataRow MiFila = Respuesta.Rows[0];

                string CodigoDB = Convert.ToString(MiFila["Codigo"]);

                if (CodigoDB == pCodigoVerificacion)
                {
                    R = true;
                }
            }
            return R;
        }

        //Edita la contraseña
        public bool EditarPassword()
        {
            bool R = false;

            try
            {
                Conexion MiCnn = new Conexion();

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Email", this.Email));

                Crypto MiEncriptador = new Crypto();
                string PasswordEncriptado = "";

                if (!string.IsNullOrEmpty(this.Contrasennia))
                {
                    PasswordEncriptado = MiEncriptador.EncriptarEnUnSentido(this.Contrasennia);
                }

                MiCnn.ListadoDeParametros.Add(new SqlParameter("@Pass", PasswordEncriptado));

                int retorno = MiCnn.DMLUpdateDeleteInsert("SPUsuarioEditarPassword");

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

    }
}
