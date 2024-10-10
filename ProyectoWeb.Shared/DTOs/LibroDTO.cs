using ProyectoWeb.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Shared.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDePublicacion { get; set; }
        public int Precio { get; set; }
        public int AutorId { get; set; }
        public int EditorialId { get; set; }
        public int CategoriaId { get; set; }
        public string NombreAutor { get; set; }
        public string NombreEditorial{ get; set; }
        public string NombreCategoria { get; set; }


    }

}

