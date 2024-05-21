using AIServices.GeneratedServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AIChatbotServices.WebApi.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class ChatBotController : ControllerBase
{
    BotClient _botClient;
    public ChatBotController(BotClient botClient)
    {
        _botClient = botClient;
    }

    [HttpPost]
    public async IAsyncEnumerable<string> SendStreamAsync(BotRequestContract botRequest)
    {
        var result = await _botClient.SendStreamAsync(botRequest);
        foreach (var item in result)
        {
            yield return item;
        }
    }

    [HttpPost]
    public Task<ChatResponseContract> CreateChat(ChatRequestContract chatRequest)
    {
        var tenantId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == nameof(chatRequest.TenantId));
        if (tenantId is null || tenantId.Value.IsNullOrEmpty())
            throw new InvalidDataException("Please send me a valid TenantId! did you login correctly?");
        chatRequest.TenantId = long.Parse(tenantId.Value);
        return _botClient.CreateChatAsync(chatRequest);
    }

    [HttpPost]
    public Task<BotResponseContract> SendAsync(BotRequestContract botRequest)
    {
        return _botClient.SendAsync(botRequest);
    }
}
