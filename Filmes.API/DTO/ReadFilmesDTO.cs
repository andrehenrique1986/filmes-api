using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmes.API.DTO
{
    public class ReadFilmesDTO
    {
       
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
