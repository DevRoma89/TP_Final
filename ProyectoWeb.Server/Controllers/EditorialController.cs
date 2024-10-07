using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server.Controllers
{
    [ApiController]
    [Route("api/editorial")]
    public class EditorialController:ControllerBase
    {
        private readonly AppDbContext context;

        public EditorialController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EditorialDTO>>> GetAll() { 
        
            var editoriales = await context.Editoriales.ToListAsync();

            return editoriales.Adapt<List<EditorialDTO>>();
        
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(EditorialDTO editorialDTO) {
        
            var existe = await context.Editoriales.AnyAsync(x => x.Nombre == editorialDTO.Nombre);

            if( existe) {

                return BadRequest("Ya existe una editorial con ese nombre");
                    
            }

            var editorial = editorialDTO.Adapt<Editorial>();

            context.Add(editorial);
            await context.SaveChangesAsync();   
            return Ok(editorial.Id);
        
        
        }

    }
}
