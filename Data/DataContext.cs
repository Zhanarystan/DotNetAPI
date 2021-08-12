using DotNetAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetAPI.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chat> Chats {get; set;}
        public DbSet<Message> Messages {get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<AppUser>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Members)
                .UsingEntity(j => j.ToTable("AppUserChat"));

            builder.Entity<Chat>()
                .HasMany(c => c.Members)
                .WithMany(m => m.Chats)
                .UsingEntity(j => j.ToTable("AppUserChat"));
            
            builder.Entity<Message>()
                .ToTable("Messages")
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Message>()
                .ToTable("Messages")
                .HasOne(m => m.Author);
        }
    }
}