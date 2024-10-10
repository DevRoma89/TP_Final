using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController : ControllerBase
    {

        private readonly AppDbContext context;

        public LibroController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Put([FromRoute] int id, [FromBody] LibroDTO libroDTO) {

            var existeLibro = await context.Libros.AnyAsync(x => x.Id == id);

            if (!existeLibro) {

                return NotFound("No se encontro un libro con ese ID");

            }

            if (!(libroDTO.Id == id)) {

                return BadRequest("El ID de la ruta no es el mismo que del cuerpo");

            }

            var libro = libroDTO.Adapt<Libro>();
            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();







        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) {

            var libro = await context.Libros.FindAsync(id);

            if (libro == null) {
                return NotFound("No se encontro un libro con este ID");
            }

            context.Remove(libro);
            await context.SaveChangesAsync();
            return Ok(libro.Id);



        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> GetAll() {

            var libros = await context.Libros
                .Include(x => x.Autor)
                .Include(x => x.Editorial)
                .Include(x => x.Categoria)
                .ToListAsync();

            return libros.Adapt<List<LibroDTO>>();


        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> GetById(int id) { 
        
            var libros = await context.Libros
                .Include(x => x.Autor)
                .Include(x => x.Editorial)
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            return libros.Adapt<LibroDTO>();
           
        
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post( LibroDTO libroDTO) { 
        
            var existeA = await context.Autores.AnyAsync(x => x.Id == libroDTO.AutorId);
            var existeE = await context.Editoriales.AnyAsync(x => x.Id == libroDTO.EditorialId);
            var existeC = await context.Categorias.AnyAsync(x => x.Id == libroDTO.CategoriaId);

            if (!existeA) {
                return NotFound("No se encontro un autor con ese ID");
            }
            if (!existeE) {
                return NotFound("No se encontro una Editorial con ese ID");
            }
            if (!existeC) {
                return NotFound("No se encontro una Categoria con ese ID");
            }
            if ( libroDTO.Precio <= 0 && libroDTO.Precio != null) {
                return BadRequest("No ingreso un precio valido");
            }

            var libro = libroDTO.Adapt<Libro>();
            context.Add(libro);
            await context.SaveChangesAsync();   
            return Ok(libro.Id);



        
        }
    }
}
