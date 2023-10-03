using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository;

public class GestaoDeProdutosContext : DbContext {

    public GestaoDeProdutosContext(DbContextOptions<GestaoDeProdutosContext> options) : 
        base(options) {}

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoDeProdutosContext).Assembly);
    }

}

