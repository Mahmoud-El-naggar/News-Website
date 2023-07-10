using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TahliliTask.DAL;

public class NewsContext: IdentityDbContext
{
    public DbSet<News> News => Set<News>();
    public DbSet<Author> Authors => Set<Author>();

    public NewsContext(DbContextOptions options):base(options)
    {
            
    }
}
