using Microsoft.EntityFrameworkCore;

namespace Dominio.Data;

public class SqlContext : DbContext
{

   public DbSet<Panel> Panels { get; set; }
   public DbSet<Task> Tasks { get; set; }
   public DbSet<Team> Teams { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Trash> Trashes { get; set; }
   public DbSet<Comment> Comments { get; set; }

   public SqlContext(DbContextOptions<SqlContext> options) : base(options)
   {
       this.Database.Migrate(); // esto es para ejecutar las migraciones.
   }  
}