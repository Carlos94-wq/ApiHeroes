using Heroes.Presentacion.Entidades;
using Heroes.Presentacion.Negocio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heroes.Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroeController : ControllerBase
    {
        private readonly N_Heroe _Negocio;

        public HeroeController(N_Heroe Negocio)
        {
            this._Negocio = Negocio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroe>>> GetAllHereoes()
        {
            List<Heroe> lst = await _Negocio.GetAllHeroes();

            if (lst.Equals(null))
            {
                return BadRequest();
            }
            else
            {
                return lst;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Heroe>> GetHeroeById(int id)
        {
            Heroe Model = await _Negocio.GetHeroeById(id);

            if (Model == null)
            {
                return BadRequest();
            }
            else
            {
                return Model;
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostHeroe([FromBody]Heroe Model)
        {
            string Response = await _Negocio.PostHeroe(Model);

            if (Response.Equals("ok"))
            {
                return Ok("Registro Completado");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteHeroe(int id)
        {
            string Response = await _Negocio.DeleteHeroe(id);

            if (Response.Equals("ok"))
            {
                return Ok("Registro Completado");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
       public async Task<ActionResult<string>> PutHeroe([FromBody]Heroe Model)
        {
            string Response = await _Negocio.PutHeroe(Model);

            if (Response.Equals("ok"))
            {
                return Ok("Registro Completado");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
