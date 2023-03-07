using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HomeAppWebAPI.Controllers
{
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

        // GET api/UserController/objectId
        //User with address and home model
        [HttpGet("{id}", Name = "GetUserFullByObjectId")]
        public async Task<ActionResult<ReadUserDTO>> GetUserFullByObjectId(int id)
        {
            try
            {
                var output = await userData.GetUserById(id);

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

                var updatedUserModel = mapper.Map(userDTO, output.FirstOrDefault());

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
}
