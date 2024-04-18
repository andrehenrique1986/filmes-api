using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filmes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Filmes.API.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts)
            : base(opts)
        {
            
        }

        public DbSet<Models.Filmes> Filmes { get; set; }
       
    }
}
