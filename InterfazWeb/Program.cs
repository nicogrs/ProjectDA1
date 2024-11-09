using DataAccess;
using Dominio.Data;
using Dominio;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using IUserDatabase = Dominio.IUserDatabase;

namespace InterfazWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<PasswordService>();
        builder.Services.AddScoped<IRepository<User>, UserDatabaseRepository>();
        builder.Services.AddScoped<TeamDataBase>();
        builder.Services.AddScoped<ITeamDataBase, TeamDataBase>();
        builder.Services.AddScoped<TeamService>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<PanelService>();
        builder.Services.AddScoped<IPanelService, PanelService>();
        builder.Services.AddScoped<TaskService>();
        builder.Services.AddScoped<Session>();
        
        builder.Services.AddDbContextFactory<SqlContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                providerOptions => providerOptions.EnableRetryOnFailure())
        );
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SqlContext>();
            
            if (!context.Users.Any(u => u.Email == "admin@taskpanel.com"))
            {
                context.Users.Add(new User
                {
                    Name = "Super",
                    Surname = "Admin",
                    Email = "admin@taskpanel.com",
                    BirthDate = new DateTime(2000, 8, 30),
                    Password = "Admin123$",
                    Admin = true,
                });
        
                context.SaveChanges();
            }
        }
        
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}