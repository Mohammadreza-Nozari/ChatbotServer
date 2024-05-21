using AIChatbotServices.Contracts.Requests;
using AIChatbotServices.Contracts.Responses;
using AIChatbotServices.Database.Entities;
using AIChatbotServices.Logics;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIChatbotServices.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    readonly AuthenticationLogic _authenticationLogic;
    readonly IEasyQueryableAsync<UserEntity> _userRepository;
    readonly IHashProvider _hashProvider;
    public AuthenticationController(AuthenticationLogic authenticationLogic, IDatabase database, IHashProvider hashProvider)
    {
        _userRepository = database.GetQueryOf<UserEntity>();
        _authenticationLogic = authenticationLogic;
        _hashProvider = hashProvider;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ObjectResult> Login(LoginRequestContract loginRequest)
    {
        string hashPassword = _hashProvider.ComputeHexString(loginRequest.Password);
        var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == loginRequest.UserName && x.Password == hashPassword);
        if (user is not null)
        {
            var tokenString = _authenticationLogic.GenerateJSONWebToken(user);
            return Ok(new LoginResponseContract()
            {
                AccessToken = tokenString
            });
        }
        return Unauthorized("UserName or Password is not valid!");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<StatusCodeResult> Register(ResigterUserRequestContract resigterUserRequest)
    {
        string hashPassword = _hashProvider.ComputeHexString(resigterUserRequest.Password);
        await _userRepository.AddAsync(new UserEntity()
        {
            Name = resigterUserRequest.Name,
            Email = resigterUserRequest.Email,
            Address = resigterUserRequest.Address,
            Description = resigterUserRequest.Description,
            Password = hashPassword,
            Phone = resigterUserRequest.Phone,
            TenantId = resigterUserRequest.TenantId,
            UserName = resigterUserRequest.UserName,
        });
        await _userRepository.SaveChangesAsync();
        return Ok();
    }
}
