using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Filmes.API.DTO;
using Filmes.API.Models;

namespace Filmes.API.Profiles 
{
    public class FilmesProfile : Profile
    {
        public FilmesProfile()
        {
            CreateMap<CreateFilmesDTO, Models.Filmes>();
            CreateMap<UpdateFilmesDTO, Models.Filmes>();
            CreateMap<Models.Filmes, UpdateFilmesDTO>();
            CreateMap<Models.Filmes, ReadFilmesDTO>();
        }

    }
}
