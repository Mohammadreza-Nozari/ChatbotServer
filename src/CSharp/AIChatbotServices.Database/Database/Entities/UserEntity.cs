using AIChatbotServices.Database.Entities.Relations;
using AICore.Interfaces;

namespace AIChatbotServices.Database.Entities;
public class UserEntity : IIdSchema
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public long TenantId { get; set; }
    public TenantEntity Tenant { get; set; }

    public ICollection<UserWidgetEntity> UserChatBots { get; set; }
}
