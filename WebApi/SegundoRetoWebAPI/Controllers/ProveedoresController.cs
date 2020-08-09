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
    public class ProveedoresController : ControllerBase
    {
        private readonly SegundoRetoContext _dbcontext;
       
        public ProveedoresController(SegundoRetoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> Get()
        {
            List<Proveedor> proveedores = await _dbcontext.Proveedores.Include(x => x.Productos).Where(x => x.Estado == 1).ToListAsync();
            return proveedores;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> Get(int id)
        {
            Proveedor proveedor = await _dbcontext.Proveedores.Include(x => x.Productos).FirstOrDefaultAsync(x => (x.Estado == 1 && x.Id == id));
            if(proveedor == null)
            {
                return NotFound();
            }
            return proveedor;
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Proveedor proveedor)
        {
            proveedor.Estado = 1;
            _dbcontext.Proveedores.Add(proveedor);
            await _dbcontext.SaveChangesAsync();
            return new CreatedAtRouteResult("", new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Proveedor proveedor)
        {
            var verificarEstadoProveedor = await _dbcontext.Proveedores.Select(x => new {x.Id, x.Estado}).Where(x => ((x.Id == id)&&(x.Estado == 1))).FirstOrDefaultAsync();
            
            if ((id != proveedor.Id)||(verificarEstadoProveedor == null))
            {
                return NotFound();
            }
            //Mantener el estado activo independientemente de lo que manden a traves de una petición
            proveedor.Estado = 1;

            _dbcontext.Entry(proveedor).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        //public async Task<ActionResult<Proveedor>> Delete(int id)
        public async Task<ActionResult> Delete(int id)
        {
            /* 
            var autorId = await _dbcontext.Proveedores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            _dbcontext.Proveedores.Remove(new Proveedor { Id = autorId });
            await _dbcontext.SaveChangesAsync();
            return NoContent(); 
            */
            Proveedor proveedor = await _dbcontext.Proveedores.FirstOrDefaultAsync(x => ((x.Id == id)&&(x.Estado==1)));
            if(proveedor == null)
            {
                return NotFound();
            }
            proveedor.Estado = 0;
            _dbcontext.Entry(proveedor).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}
