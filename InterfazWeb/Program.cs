using Dominio.Data;
using Dominio;

namespace InterfazWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddSingleton<UserDataBase>();
        builder.Services.AddSingleton<IUserDatabase, UserDataBase>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<PasswordService>();
        builder.Services.AddSingleton<TeamDataBase>();
        builder.Services.AddSingleton<ITeamDataBase, TeamDataBase>();
        builder.Services.AddSingleton<TeamService>();
        builder.Services.AddSingleton<PanelService>();
        builder.Services.AddSingleton<TaskService>();
        builder.Services.AddSingleton<Session>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}