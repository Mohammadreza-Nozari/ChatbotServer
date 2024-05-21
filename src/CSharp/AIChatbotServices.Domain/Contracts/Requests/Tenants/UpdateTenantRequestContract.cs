namespace AIChatbotServices.Contracts.Requests.Tenants;
public class UpdateTenantRequestContract
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
