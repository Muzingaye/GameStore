using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.EndPoints;


public static class Games
{
    const string EndpointName = "GetName";

    static readonly List<GameDto> games = [
       new (1, "Street Fighter II", "Fighting", 10.99M, new DateOnly(1999, 7,15)),
       new (2, "Final Stantacy VII Rebirth", "RPG", 99.99M, new DateOnly(1999, 7,15)),
       new (3, "Street Figher V", "Platform", 10.99M, new DateOnly(1999, 7,15))
     ];


    public static void MapGameEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", async (GameStoreContext context) => await context.Games
        .Include(game => game.Genre)
        .Select(game => new GameDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        )).AsNoTracking().ToListAsync());
        group.MapGet("/{id}", async (int id, GameStoreContext context) =>
        {
            var game = await context.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(new GameDetailDto(
game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate
            ));
        })
        .WithName(EndpointName);

        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext context) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate,
            };
            context.Games.Add(game);
            await context.SaveChangesAsync();
            GameDetailDto gameDto = new(
                game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate
            );
            return Results.CreatedAtRoute(EndpointName, new { id = gameDto.Id }, gameDto);
        });


        group.MapPut("/{id}", async (int id, UpdateGameDto update, GameStoreContext context) =>
        {
            var gam = await context.Games.FindAsync(id);
            gam.Name = update.Name;
            gam.GenreId = update.GenreId;
            gam.Price = update.Price;
            gam.ReleaseDate = update.ReleaseDate;
            await context.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            int removed = games.RemoveAll(g => g.Id == id);
            return removed == 0 ? Results.NotFound() : Results.NoContent();
        });
    }
}