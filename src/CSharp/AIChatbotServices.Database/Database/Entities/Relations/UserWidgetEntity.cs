using AICore.Interfaces;

namespace AIChatbotServices.Database.Entities.Relations;
public class UserWidgetEntity : IIdSchema
{
    public long Id { get; set; }

    public string Name { get; set; }
    public string Color { get; set; }

    public long UserId { get; set; }
    public long ChatBotId { get; set; }

    public UserEntity User { get; set; }
    public ChatBotEntity ChatBot { get; set; }
}
