using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    public class CategoriaController: ControllerBase
    {

        private readonly AppDbContext context;

        public CategoriaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> GetAll()
        {

            var categorias = await context.Categorias.ToListAsync();

            return categorias.Adapt<List<CategoriaDTO>>();


        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CategoriaDTO categoriaDTO) { 
        
            var existe = await context.Categorias.AnyAsync(x => x.Nombre == categoriaDTO.Nombre);

            if ( existe ) {

                return BadRequest("Ya existe una categoria con ese nombre");
                
            }

            var categoria = categoriaDTO.Adapt<Categoria>(); 

            context.Add(categoria);
            await context.SaveChangesAsync();   
            return Ok(categoria.Id);
        }
    }
}
