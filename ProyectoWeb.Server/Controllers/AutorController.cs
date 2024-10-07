using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;

namespace ProyectoWeb.Server.Controllers
{
    [ApiController]
    [Route("api/autor")]
    public class AutorController:ControllerBase
    {

        private readonly AppDbContext context;

        public AutorController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> GetAll() { 
        
            var autores = await context.Autores.ToListAsync();

            return autores.Adapt<List<AutorDTO>>();
        
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(AutorDTO autorDTO) {
        
            var autor = autorDTO.Adapt<Autor>();

            context.Add(autor);
            await context.SaveChangesAsync();   
            return Ok(autor.Id);
        
        }

    }
}
