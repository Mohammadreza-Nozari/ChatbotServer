using AIChatbotServices.Database.Entities.Relations;
using AICore.Interfaces;

namespace AIChatbotServices.Database.Entities;
public class ChatBotEntity : IIdSchema
{
    public long Id { get; set; }
    public string Name { get; set; }

    public ICollection<UserWidgetEntity> UserChatBots { get; set; }
}
