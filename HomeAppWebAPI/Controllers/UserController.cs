using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;
using HomeAppWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Serilog;

namespace HomeAppWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserData userData;
    private readonly IMapper mapper;

    public UserController(IUserData userData, IMapper mapper)
    {
        this.userData = userData;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        AuthConfig config = AuthConfig.ReadFromJsonFile("appsettings.json");

        IConfidentialClientApplication app;

        app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
        .WithClientSecret(config.ClientSecret)
        .WithAuthority(new Uri(config.Authority))
        .Build();

        string[] ResourceIds = new string[] { config.ResourceID };

        AuthenticationResult result = null;

        try
        {
            result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();

            if (!String.IsNullOrEmpty(result.AccessToken))
            {
                return Ok(result.AccessToken);
            }

            return NoContent();

        }
        catch (MsalClientException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // GET api/UserController/objectId
    //User with address and home model
    [HttpGet("{id}", Name = "GetUserFullByObjectId")]
    
    public async Task<ActionResult<ReadUserDTO>> GetUserFullByObjectId(string id)
    {
        try
        {
            var output = await userData.GetUserFullByObjectId(id);

            if (output == null)
            {
                return NotFound();
            }


            return Ok(output);
        }
        catch (Exception ex)
        {

            Log.Information("Error by a GetUserFullByObjectId Action: {ex}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ReadUserDTO>> CreateUser([FromBody] CreateUserDTO createUser)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid User model");
            }

            var userModel = mapper.Map<UserModel>(createUser);

            await userData.CreateUser(createUser);

            var readUserDto = mapper.Map<ReadUserDTO>(userModel);

            return CreatedAtRoute(nameof(GetUserFullByObjectId), new { Id = readUserDto.Id }, readUserDto);
        }
        catch (Exception ex)
        {
            Log.Information("Error by a CreateUser Action: {ex}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("/login")]
    public async Task<ActionResult<string>> LoginUser(string username, string password)
    {
        return Ok("token");
    }


    // DELETE api/UserController/5
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        try
        {
            userData.DeleteUser(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Information("Error by a DeleteUser Action: {ex}", ex);
            return StatusCode(500, "Internal server error");
        }
    }

    //UPDATE api/UserController/User
    [HttpPut]
    public async Task<ActionResult<ReadUserDTO>> UpdateUser(int id, [FromBody] UpdateUserDTO userDTO)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid User model");
            }

            var output = await userData.GetUserById(id);

            if (output == null)
            {
                return NotFound();
            }

            var updatedUserModel = mapper.Map(userDTO, output);

            await userData.UpdateUser(mapper.Map<UserModel>(updatedUserModel));

            return CreatedAtRoute(nameof(GetUserFullByObjectId), new { Id = updatedUserModel.Id }, updatedUserModel);
        }
        catch (Exception ex)
        {
            Log.Information("Error by a UpdateUser Action: {ex}", ex);
            return StatusCode(500, "Internal server error");
        }
    }
}
