using GameStore.api.Data;
using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.api.Mapping;

namespace GameStore.api.Endpoints;
public static class GameEndpoints
{
    const String GetGameEndpoint = "GetGame";

    private static readonly List<GameSummaryDto> games = [
    new (
        1,
        "Halo : Combat Evolved",
        "Sci-Fi",
        60.0M,
        new DateOnly(2001, 11, 15)),
    new (
        2,
        "Halo 2",
        "Sci-Fi",
        60.0M,
        new DateOnly(2004, 11, 2)),
    new (
        3,
        "Halo 3",
        "Sci-Fi",
        60.0M,
        new DateOnly (2007, 9, 25)
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");

        group.MapGet("/", () => games);
        // GET /games
        // returns whatever is in the games list that is stored in memory
        // this is called minimal api

        group.MapGet("/{id}", (int id, GameStoreContext DbContext) => // dependency injection
        {
            Game? game = DbContext.Games.Find(id);
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpoint);

        // POST

        group.MapPost("/", (CreateGameDto newGame, GameStoreContext DbContext) =>
        {
            /*GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);
            */

            Game game = newGame.ToEntity();

            /*Game game = new()
            {
                Name = newGame.Name,
                Genre = DbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            */

            //  games.Add(game);
            DbContext.Games.Add(game);
            DbContext.SaveChanges();

            /*GameDto gameDto = new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
            */

            return Results.CreatedAtRoute(
                GetGameEndpoint,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(games => games.id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameSummaryDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        //DELETE /games

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.id == id);
            return Results.NoContent();
        });

        return group;
    }
}


