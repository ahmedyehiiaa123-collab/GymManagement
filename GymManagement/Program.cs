using GymManagement.BLL;
using GymManagement.BLL.Services.Classes;
using GymManagement.BLL.Services.Classes.Interfaces;
using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Resporitory.Classes;
using GymManagement.DAL.Resporitory.Inrerface;
using GymManagement.PL;
using Microsoft.EntityFrameworkCore;

namespace GymManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbcontext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenerricReposirtory<>));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IMemberServices, MemeberService>();

            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<ISessionService, SessionService>();

            var app = builder.Build();

            await app.MigrateandSeedASync();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Session}/{action=Index}/{id?}")
    .WithStaticAssets();

            app.Run();
        }
    }
}