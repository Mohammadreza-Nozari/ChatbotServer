﻿namespace AIChatbotServices.Contracts.Requests;
public class ResigterUserRequestContract
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public long TenantId { get; set; }
}
