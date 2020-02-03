using API.AssistCard.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace API.AssistCard.Infrastructure
{
    public class ApiAssistCardContext : DbContext
    {
        public ApiAssistCardContext(DbContextOptions<ApiAssistCardContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }        

    }
}
