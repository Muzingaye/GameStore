using GameStore.Api.Dtos;

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

        group.MapGet("/", () => games);
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(EndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
        games.Count() + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
            games.Add(game);
            return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
        });


        group.MapPut("/{id}", (int id, UpdateGameDto update) =>
        {
            var idx = games.FindIndex(g => g.Id == id);
            games[idx] = new GameDto(
      id, update.Name, update.Genre, update.Price, update.ReleaseDate
    );

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            int removed = games.RemoveAll(g => g.Id == id);
            return removed == 0 ? Results.NotFound() : Results.NoContent();
        });
    }
}