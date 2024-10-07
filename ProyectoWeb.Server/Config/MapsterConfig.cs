using Mapster;
using ProyectoWeb.Shared.DTOs;
using ProyectoWeb.Shared.Entidades;
using System.Reflection;

namespace ProyectoWeb.Server.Config
{
    public static class MapsterConfig
    {
        public static void MapsterConfiguration(this IServiceCollection services) {

            TypeAdapterConfig<Libro, LibroDTO>
                .NewConfig()
                .Map(dest => dest.NombreAutor, src => $"{src.Autor.Nombre} {src.Autor.Apellido}")
                .Map(dest => dest.NombreEditorial, src => src.Editorial.Nombre)
                .Map(dest => dest.NombreCategoria, src => src.Categoria.Nombre);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        
        }
    }
}
