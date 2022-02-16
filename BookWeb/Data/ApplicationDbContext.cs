using BookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
}
