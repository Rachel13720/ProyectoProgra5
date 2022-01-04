using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaProyecto
{
    public class Venta
    {
        //atributos
        public int IDVenta { get; set; }

        public DateTime Fecha { get; set; }

        public string NumeroFactura { get; set; }

        public string Comentario { get; set; }

        public bool Activo { get; set; }

        public List<VentaDetalle> ListaDetalles { get; set; }

        public Usuario UsuarioRegistra { get; set; }

        public Cliente MiCliente { get; set; }

        public Venta()
        {
            ListaDetalles = new List<VentaDetalle>();
            UsuarioRegistra = new Usuario();
            MiCliente = new Cliente();
            Activo = true;
        }

        //Agrega los datos de la venta en la BD
        public bool Agregar()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Fecha", this.Fecha));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@NumeroFactura", this.NumeroFactura));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@Comentario", this.Comentario));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdUsuario", this.UsuarioRegistra.IDUsuario));
            MiCnn.ListadoDeParametros.Add(new SqlParameter("@IdCliente", this.MiCliente.IDCliente));

            Object Retorno = MiCnn.DMLConRetornoEscalar("SPVentaAgregarEncabezado"); 

            int IdVentaRecienCreada;

            //si lo que se retorna no esta vacio entonces agrega los datos a la lista
            if (Retorno != null)
            {
                try
                {
                    IdVentaRecienCreada = Convert.ToInt32(Retorno.ToString());

                    this.IDVenta = IdVentaRecienCreada;

                    int Acumulador = 0;

                    foreach (VentaDetalle item in this.ListaDetalles)
                    {
                        Conexion MyCnnDet = new Conexion();

                        MyCnnDet.ListadoDeParametros.Add(new SqlParameter("@IdProd", item.MiProducto.IDProducto));
                        MyCnnDet.ListadoDeParametros.Add(new SqlParameter("@IdVenta", this.IDVenta));
                        MyCnnDet.ListadoDeParametros.Add(new SqlParameter("@Cant", item.CantidadVendida));
                        MyCnnDet.ListadoDeParametros.Add(new SqlParameter("@Precio", item.PrecioVenta));

                        MyCnnDet.DMLUpdateDeleteInsert("SPVentaAgregarDetalle");

                        Acumulador += 1;

                    }

                    if (Acumulador == this.ListaDetalles.Count)
                    {

                        R = true;

                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }

            return R;
        }

        //Anula la venta 
        public bool Anular()
        {
            bool R = false;

            //codigo permite anular venta
            //en la base datos

            return R;
        }

        //consulta la venta por el id en la BD
        public bool ConsultarPorID(int IDVenta)
        {
            bool R = false;

            return R;

        }

        //consulta la venta por numero de factura y cliente en la BD
        public bool ConsultarPorNumeroFacturaYCliente(string NumeroFactura, int IDCliente)
        {
            bool R = false;

            return R;
        }

        //Lista todos los clientes en la BD
        public DataTable ListarTodas()
        {
            DataTable R = new DataTable();

            return R;
        }

        //asigna el esquema de detalle en la BD
        public DataTable AsignarEsquemaDetalle()
        {
            DataTable R = new DataTable();

            Conexion MyCnn = new Conexion();

            R = MyCnn.DMLSelect("SPVentaDetalleSchema", true);

            return R;
        }

        //lista los datos por fecha en la BD
        public DataTable ListarPorFecha(DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable R = new DataTable();

            return R;
        }

        //lista los datos por cliente en la BD
        public DataTable ListarPorCliente(int IDCliente, DateTime? FechaInicio = null, DateTime? FechaFin = null)
        {
            DataTable R = new DataTable();

            return R;
        }

        //Imprime el reporte de la venta, utiliza el procedimiento SPVentaReporte en la BD
        public ReportDocument Imprimir(ReportDocument repo)
        {
            ReportDocument R = repo;

            Crystal ObjCrystal = new Crystal(R);

            DataTable Datos = new DataTable();

            Conexion MiCnn = new Conexion();

            MiCnn.ListadoDeParametros.Add(new SqlParameter("@ID", this.IDVenta));

            Datos = MiCnn.DMLSelect("SPVentaReporte");

            if (Datos != null && Datos.Rows.Count > 0)
            {
                ObjCrystal.Datos = Datos;

                R = ObjCrystal.GenerarReporte();
            }


            return R;

        }
    }
}
