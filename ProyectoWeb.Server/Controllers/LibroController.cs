using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server.Controllers
{
    [ApiController]
    [Route("api/libro")]
    public class LibroController:ControllerBase
    {

        private readonly AppDbContext context;

        public LibroController(AppDbContext context)
        {
            this.context = context;
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

            var libro = libroDTO.Adapt<Libro>();
            context.Add(libro);
            await context.SaveChangesAsync();   
            return Ok(libro.Id);



        
        }
    }
}
