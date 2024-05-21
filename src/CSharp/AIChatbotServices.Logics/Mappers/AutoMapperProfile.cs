using AIChatbotServices.Contracts.Common.Tenants;
using AIChatbotServices.Contracts.Requests;
using AIChatbotServices.Contracts.Requests.Tenants;
using AIChatbotServices.Database.Entities;
using AutoMapper;

namespace AIChatbotServices.Mappers;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TenantEntity, TenantContract>();
        CreateMap<CreateTenantRequestContract, TenantEntity>();

        CreateMap<ResigterUserRequestContract, UserEntity>();
    }
}
