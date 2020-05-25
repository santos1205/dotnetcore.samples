using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oauth_sln.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sabor>().HasData(
                new Sabor
                {
                    Id = 1,
                    Descricao = "Castanha"
                },
                new Sabor
                {
                    Id = 2,
                    Descricao = "Chocolate"
                },
                new Sabor
                {
                    Id = 3,
                    Descricao = "Casadinho (goiaba)"
                }
            );

        }
    }
}
