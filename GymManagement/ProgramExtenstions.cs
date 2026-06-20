using GymManagement.DAL.Data.DataSeeding;
using GymManagement.DAL.Data.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.PL
{
    public  static class ProgramExtenstions
    {
        public static async Task MigrateandSeedASync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<GymDbcontext>();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            var pendingmigration = await dbcontext.Database.GetPendingMigrationsAsync();
                if (pendingmigration.Any()) {
                logger.LogInformation($"Applying {pendingmigration.Count()}Migration");
                await dbcontext.Database.MigrateAsync();
            }

            

            var seedingpath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "Files");
      await GymDataSeeding.SeedAsync(dbcontext,seedingpath, logger);
        }

    }
}
