using GameStore.Api.EndPoints;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

class GameStoreContext(DbContextOptions<GameStoreContext> opt) : DbContext(opt)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}