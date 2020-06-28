using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiPersonas.Presentacion.Datos
{
    public class Contexto
    {
        private string ConnectionStrings { get; }

        public Contexto(IConfiguration config)
        {
            this.ConnectionStrings = config.GetConnectionString("DefaultConnection");
        }

        public async Task<SqlDataReader> ExecuteReader(string sentencia, List<SqlParameter> Parametro = null)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection Con = new SqlConnection(ConnectionStrings);

            try
            {
                await Con.OpenAsync();

                comando.Connection = Con;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = sentencia;

                if (Parametro != null)
                {
                    comando.Parameters.AddRange(Parametro.ToArray());
                }

                SqlDataReader Dr = await comando.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                return Dr;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> ExecureNonQuery(string sentencia, List<SqlParameter> Parametro = null)
        {
            SqlCommand comando = new SqlCommand();
            var Respuesta = "";

            try
            {
                using (SqlConnection Con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        await Con.OpenAsync();

                        comando.Connection = Con;
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.CommandText = sentencia;

                        if (Parametro != null)
                        {
                            comando.Parameters.AddRange(Parametro.ToArray());

                            int ExecuteAwait = await comando.ExecuteNonQueryAsync();

                            if (ExecuteAwait == 1) // Si la ejecucion se realiza se modifinca en uno, quiere decir que si se pudo
                            {
                                Respuesta = "ok";// retorna un ok
                            }
                        }
                    }
                    return Respuesta;
                }
            }
            catch (SqlException e)
            {
                return Respuesta = e.Message;
            }
        }
    }
}
