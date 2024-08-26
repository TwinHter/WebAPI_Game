using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStoreContext : 
    DbContext
{
    public GameStoreContext(DbContextOptions options) : base(options) {
    }
    public DbSet<Game> Games => Set<Game>(); // Expression-bodied members
    public DbSet<Genre> Genres=> Set<Genre>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Fighting"},
            new { Id = 2, Name = "Shooting"},
            new { Id = 3, Name = "Moba"},
            new { Id = 4, Name = "Multiplayers"}
        );
    }
}