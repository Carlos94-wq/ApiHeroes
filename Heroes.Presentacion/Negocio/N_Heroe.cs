using ApiPersonas.Presentacion.Datos;
using Heroes.Presentacion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Heroes.Presentacion.Negocio
{
    public class N_Heroe
    {
        private readonly Contexto _Contexto;

        public N_Heroe(Contexto Contexto)
        {
            this._Contexto = Contexto;
        }

        public async Task<List<Heroe>> GetAllHeroes()
        {
            List<Heroe> LstHeroe = new List<Heroe>();
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(new SqlParameter("@Accion", 1));

            SqlDataReader Dr = await _Contexto.ExecuteReader("spHeroes", Parameters);

            while (await Dr.ReadAsync())
            {
                Heroe Model = new Heroe();
                Model.IdHeroe = (int)Dr["IdHeroe"];
                Model.Nombre = Dr["Nombre"].ToString();
                Model.Poder = Dr["Poder"].ToString();
                Model.Vivo = (bool)Dr["Vivo"];

                LstHeroe.Add(Model);
            }

            return LstHeroe;
        }

        public async Task<Heroe> GetHeroeById(int id)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(new SqlParameter("@Accion", 2));
            Parameters.Add(new SqlParameter("@IdHeroe", id));

            SqlDataReader Dr = await _Contexto.ExecuteReader("spHeroes", Parameters);

            Heroe Model = new Heroe();

            while (await Dr.ReadAsync())
            {
               
                Model.IdHeroe = (int)Dr["IdHeroe"];
                Model.Nombre = Dr["Nombre"].ToString();
                Model.Poder = Dr["Poder"].ToString();
                Model.Vivo = (bool)Dr["Vivo"];
            }

            return Model;
        }

        public async Task<string> PostHeroe(Heroe Model)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(new SqlParameter("@Accion", 3));
            Parameters.Add(new SqlParameter("@Nombre", Model.Nombre));
            Parameters.Add(new SqlParameter("@Poder", Model.Poder));
            Parameters.Add(new SqlParameter("@Vivo", Model.Vivo));

            string Response = await _Contexto.ExecureNonQuery("spHeroes", Parameters);

            return Response;
        }

        public async Task<string> DeleteHeroe(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Accion", 4));
            parameters.Add(new SqlParameter("@IdHeroe", id));

            string Response = await _Contexto.ExecureNonQuery("spHeroes", parameters);

            return Response;
        }

        public async Task<string> PutHeroe(Heroe Model)
        {
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(new SqlParameter("@Accion", 5));
            Parameters.Add(new SqlParameter("@Nombre", Model.Nombre));
            Parameters.Add(new SqlParameter("@Poder", Model.Poder));
            Parameters.Add(new SqlParameter("@Vivo", Model.Vivo));
            Parameters.Add(new SqlParameter("@IdHeroe", Model.IdHeroe));

            string Response = await _Contexto.ExecureNonQuery("spHeroes", Parameters);

            return Response;
        }
    }
}
