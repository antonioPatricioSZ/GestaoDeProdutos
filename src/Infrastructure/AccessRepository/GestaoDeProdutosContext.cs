using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AccessRepository;

public class GestaoDeProdutosContext : DbContext {

    public GestaoDeProdutosContext(DbContextOptions<GestaoDeProdutosContext> options) : 
        base(options) {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoDeProdutosContext).Assembly);
    }

}

