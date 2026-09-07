using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.EndPoints;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

builder.AddGamesStore();

var app = builder.Build();
app.MapGameEndPoints();

app.MigrateDb();
app.Run();
