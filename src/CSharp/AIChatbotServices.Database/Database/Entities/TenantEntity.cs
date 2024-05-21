using AICore.Interfaces;

namespace AIChatbotServices.Database.Entities;
public class TenantEntity : IIdSchema
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<UserEntity> Customers { get; set; }
}
