using Microsoft.EntityFrameworkCore;
using Task = Dominio.Task;
namespace DataAccess;
using Dominio;
public class SqlContext : DbContext
{

   public DbSet<Panel> Panels { get; set; }
   public DbSet<Task> Tasks { get; set; }
   public DbSet<Team> Teams { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Trash> Trashes { get; set; }
   public DbSet<Comment> Comments { get; set; }
   public DbSet<Notification> Notifications { get; set; }

   public SqlContext(DbContextOptions<SqlContext> options) : base(options)
   {
       if (!Database.IsInMemory())
       {
           Database.Migrate();
       }
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       modelBuilder.Entity<IDeleteable>().UseTpcMappingStrategy();
     
       modelBuilder.Entity<Task>()
           .ToTable("Tasks")
           .HasBaseType<IDeleteable>();
       
       modelBuilder.Entity<Panel>()
           .ToTable("Panels")
           .HasBaseType<IDeleteable>();
       
       modelBuilder.Entity<Task>()
           .HasMany(t => t.Comments)
           .WithOne(c => c.Task)
           .HasForeignKey(c => c.TaskId)
           .OnDelete(DeleteBehavior.Cascade);
       
       modelBuilder.Entity<Panel>()
           .HasMany(p => p.Tasks)
           .WithOne(t => t.Panel)
           .HasForeignKey(t => t.PanelId)
           .OnDelete(DeleteBehavior.Cascade);
       
       modelBuilder.Entity<Notification>()
           .HasOne(n => n.FromUser)
           .WithMany()
           .HasForeignKey(t => t.FromUserId)
           .OnDelete(DeleteBehavior.NoAction);
       
       modelBuilder.Entity<Notification>()
           .HasOne(n => n.ToUser)
           .WithMany()
           .HasForeignKey(t => t.ToUserId)
           .OnDelete(DeleteBehavior.Cascade);
   }
}