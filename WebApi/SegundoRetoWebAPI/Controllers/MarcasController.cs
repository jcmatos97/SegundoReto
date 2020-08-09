using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SegundoRetoWebAPI.DataBaseCotext;
using SegundoRetoWebAPI.Models;

namespace SegundoRetoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly SegundoRetoContext _dbcontext;
       
        public MarcasController(SegundoRetoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Marca>>> Get()
        {
            List<Marca> marcas = await _dbcontext.Marcas.Include(x => x.Productos).Where(x => x.Estado == 1).ToListAsync();
            return marcas;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> Get(int id)
        {
            Marca marca = await _dbcontext.Marcas.Include(x => x.Productos).FirstOrDefaultAsync(x => (x.Estado == 1 && x.Id == id));
            if(marca == null)
            {
                return NotFound();
            }
            return marca;
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Marca marca)
        {
            marca.Estado = 1;
            _dbcontext.Marcas.Add(marca);
            await _dbcontext.SaveChangesAsync();
            return new CreatedAtRouteResult("", new { id = marca.Id }, marca);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Marca marca)
        {
            var verificarEstadoMarca = await _dbcontext.Marcas.Select(x => new {x.Id, x.Estado}).Where(x => ((x.Id == id)&&(x.Estado == 1))).FirstOrDefaultAsync();
            
            if ((id != marca.Id)||(verificarEstadoMarca == null))
            {
                return NotFound();
            }
            //Mantener el estado activo independientemente de lo que manden a traves de una petición
            marca.Estado = 1;

            _dbcontext.Entry(marca).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        //public async Task<ActionResult<Marca>> Delete(int id)
        public async Task<ActionResult> Delete(int id)
        {
            /* 
            var autorId = await _dbcontext.Marcas.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            _dbcontext.Marcas.Remove(new Marca { Id = autorId });
            await _dbcontext.SaveChangesAsync();
            return NoContent(); 
            */
            Marca marca = await _dbcontext.Marcas.FirstOrDefaultAsync(x => ((x.Id == id)&&(x.Estado==1)));
            if(marca == null)
            {
                return NotFound();
            }
            marca.Estado = 0;
            _dbcontext.Entry(marca).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}
