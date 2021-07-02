using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Seguros.Proseg.Infrastructure.Data
{
    public class MultiSegurosContext : DbContext
    {

        public MultiSegurosContext(DbContextOptions<MultiSegurosContext> options) : base(options)
        {

        }

    }
}
