using Microsoft.EntityFrameworkCore;

namespace Dominio.Data;

public class SqlContext : DbContext
{

   public DbSet<Panel> Panels { get; set; }
   public DbSet<Task> Tasks { get; set; }
   public DbSet<Team> Teams { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Trash> Trashes { get; set; }
   public DbSet<DeletedTask> DeletedTasks { get; set; }
   public DbSet<DeletedPanel> DeletedPanels { get; set; }
   public DbSet<Comment> Comments { get; set; }

   public SqlContext(DbContextOptions<SqlContext> options) : base(options)
   {
       this.Database.Migrate(); // esto es para ejecutar las migraciones.
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       modelBuilder.Entity<DeletedPanel>()
           .HasOne(dp => dp.Panel)
           .WithMany()
           .HasForeignKey(dp => dp.PanelId)
           .OnDelete(DeleteBehavior.NoAction);

       modelBuilder.Entity<DeletedPanel>()
           .HasOne(dp => dp.Trash)
           .WithMany()
           .HasForeignKey(dp => dp.TrashId)
           .OnDelete(DeleteBehavior.NoAction);
   }
}