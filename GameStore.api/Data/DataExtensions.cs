using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        //request asp.net to give us an instance of some services
        var DbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        DbContext.Database.Migrate();
    }
}
