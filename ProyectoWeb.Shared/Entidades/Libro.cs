using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWeb.Shared.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDePublicacion { get; set; }
        public int Precio { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public int EditorialId { get; set; }
        public Editorial Editorial { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }


    }
}
