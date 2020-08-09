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
    public class ProductosController : ControllerBase
    {
        private readonly SegundoRetoContext _dbcontext;
       
        public ProductosController(SegundoRetoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            List<Producto> productos = await _dbcontext.Productos.Include(x => x.Marca).Include(x => x.Proveedor).Where(x => x.Estado == 1).ToListAsync();
            return productos; 
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            Producto producto = await _dbcontext.Productos.Include(x => x.Marca).Include(x => x.Proveedor).FirstOrDefaultAsync(x => (x.Estado == 1 && x.Id == id));
            if(producto == null)
            {
                return NotFound();
            }
            return producto;
        } 

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto producto)
        {
            producto.Estado = 1;
            _dbcontext.Productos.Add(producto);
            await _dbcontext.SaveChangesAsync();
            return new CreatedAtRouteResult("", new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Producto producto)
        {
            var verificarEstadoProducto = await _dbcontext.Productos.Select(x => new {x.Id, x.Estado}).Where(x => ((x.Id == id)&&(x.Estado == 1))).FirstOrDefaultAsync();
            
            if ((id != producto.Id)||(verificarEstadoProducto == null))
            {
                return NotFound();
            }
            //Mantener el estado activo independientemente de lo que manden a traves de una petición
            producto.Estado = 1;

            _dbcontext.Entry(producto).State = EntityState.Modified;
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
            Producto producto = await _dbcontext.Productos.FirstOrDefaultAsync(x => ((x.Id == id)&&(x.Estado==1)));
            if(producto == null)
            {
                return NotFound();
            }
            producto.Estado = 0;
            _dbcontext.Entry(producto).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}
