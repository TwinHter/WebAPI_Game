using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Endpoints;

public static class GamesEndpoints {
    const string GetGameByID = "GetGameByID";
    private static readonly List<GameSummaryDto> games = [
        new(
            1,
            "League of Legend",
            "Multiplayer",
            10M,
            new DateOnly(2010, 1, 1)
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {
        var group = app.MapGroup("games")
                    .WithParameterValidation();
        // GET /games
        group.MapGet("/", (GameStoreContext dBContext) => 
            dBContext.Games.Include(game => game.Genre)
                           .Select(game => game.ToGameSummaryDto())
                           .AsNoTracking()
        );

        // GET games by id
        group.MapGet("/{id}", (int id, GameStoreContext dBContext) => {
            Game? game = dBContext.Games.Find(id);
            return (game is null) ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameByID);

        // POST game
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dBContext) => {
            Game game = newGame.ToEntity();

            dBContext.Games.Add(game);
            dBContext.SaveChanges();
            return Results.CreatedAtRoute(GetGameByID, new{id = game.Id}, game.ToGameDetailsDto());
        }); 
        // PUT game

        group.MapPut("/{id}", (int id, UpdateGameDto updateGame, GameStoreContext dBContext) => {
            Game? existingGame = dBContext.Games.Find(id);
            if(existingGame == null) {
                return Results.BadRequest();
            }
            dBContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updateGame.ToEntity(id));
            dBContext.SaveChanges();
            return Results.NoContent();
        });

        //DELETE game
        group.MapDelete("/{id}", (int id, GameStoreContext dBContext) => {
            dBContext.Games.Where(game => game.Id == id)
                           .ExecuteDelete();
            return Results.NoContent();
        });

        return group;
    }
}