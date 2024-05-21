using AIChatbotServices.Database.Entities;
using AIChatbotServices.Database.Entities.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AIChatbotServices.Database.Contexts;
public class ChatBotContext : DbContext
{
    readonly IConfiguration _configuration;
    public ChatBotContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<TenantEntity> Tenants { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Tenant)
                .WithMany(x => x.Customers);
            e.HasData(new UserEntity()
            {
                Id = 1,
                UserName = "ali",
                Password = "40bd001563085fc35165329ea1ff5c5ecbdbbeef",
                TenantId = 1
            });
        });

        modelBuilder.Entity<TenantEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasData(new TenantEntity()
            {
                Id = 1,
                Name = "Ali",
                Description = "desc"
            });
        });

        modelBuilder.Entity<ChatBotEntity>(e =>
        {
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<UserWidgetEntity>(e =>
        {
            e.HasOne(x => x.User)
                .WithMany(x => x.UserChatBots);

            e.HasOne(x => x.ChatBot)
                .WithMany(x => x.UserChatBots);
        });
    }
}
