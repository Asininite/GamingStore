using GameStore.api.Data;
using GameStore.api.Endpoints;
var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);
// registering DbContext GameStoreContext into service provider which is a service container
// gamestorecontext is getting registered with service provider so that later on anywhere in our code base
// we can go ahead and inject that instance and take advantage of its services

var app = builder.Build();

app.MapGamesEndpoints();
app.MigrateDb();
app.Run();


