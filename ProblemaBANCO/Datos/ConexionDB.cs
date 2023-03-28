using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ProblemaBANCO.Dominio;
using System.Windows.Forms;

namespace ProblemaBANCO.Datos
{
    internal class ConexionDB
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public ConexionDB()
        {
            cnn = new SqlConnection(Properties.Resources.String1);
        }
        public DataTable ConsultarSP(string SP, Cliente cliente, string dni)
        {
            cnn.Open();
            cmd = new SqlCommand(SP,cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if(SP== "CONSULTAR_CUENTAS")
            {
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
            }
            if (SP == "VERIFICAR_CLIENTE")
                cmd.Parameters.AddWithValue("@dni", dni);
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            cnn.Close();
            return table;
        }
        public bool AltaCuenta(Cliente cliente)
        {
            bool resul = true;
            SqlTransaction trs = null;
            try
            {
                cnn.Open();
                trs = cnn.BeginTransaction();
                cmd = new SqlCommand("REGISTRAR_CLIENTE", cnn, trs);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", cliente.DNI);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                SqlParameter param = cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                int idCliente = (int)param.Value;
                for (int i = 0; i < cliente.Cuentas.Count; i++)
                {
                    cmd = new SqlCommand("REGISTRA_DETALLE", cnn, trs);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@tipo_cuenta", cliente.Cuentas[i].TipoCuenta.IdTipoCuenta);
                    cmd.Parameters.AddWithValue("@saldo", cliente.Cuentas[i].Saldo);
                    cmd.Parameters.AddWithValue("@ult_mov", cliente.Cuentas[i].UltimoMovimiento);
                    cmd.Parameters.AddWithValue("@cbu", cliente.Cuentas[i].CBU);
                    cmd.ExecuteNonQuery();
                }
                trs.Commit();
            }
            catch (Exception ex)
            {
                trs.Rollback();
                resul = false;
                MessageBox.Show("Error :" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return resul;
        }
        public DataTable VerificarCliente(Cliente cliente)
        {
            DataTable dt = new DataTable();
            cnn.Open();
            cmd = new SqlCommand("VERIFICAR_CLIENTE", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dni", cliente.DNI);
            dt.Load(cmd.ExecuteReader());
            cnn.Close();
            return dt;
        }
    }
}
