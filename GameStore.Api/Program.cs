using GameStore.Api.Dtos;
using GameStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
var app = builder.Build();
app.MapGameEndPoints();
app.Run();
