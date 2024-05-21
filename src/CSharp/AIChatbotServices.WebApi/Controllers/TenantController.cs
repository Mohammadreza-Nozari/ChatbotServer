using AIChatbotServices.Contracts.Common.Tenants;
using AIChatbotServices.Contracts.Requests.Tenants;
using AIChatbotServices.Database.Entities;
using AICore.WebApi.Controllers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AIChatbotServices.Controllers;

[Authorize]
public class TenantController : BaseController<TenantEntity, CreateTenantRequestContract, UpdateTenantRequestContract, TenantContract>
{
    public TenantController(IMapperProvider mapper, IDatabase database)
        : base(mapper, database)
    {
    }
}
