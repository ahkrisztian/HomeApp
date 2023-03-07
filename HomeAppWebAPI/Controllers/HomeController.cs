using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IHomeDataAccess homeDataAccess;
        private readonly IMapper mapper;

        public HomeController(IHomeDataAccess homeDataAccess, IMapper mapper)
        {
            this.homeDataAccess = homeDataAccess;
            this.mapper = mapper;
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}", Name = "GetHomeModelById")]
        public async Task<ActionResult<HomeModel>> GetHomeModelById(int id)
        {
            try
            {
                var output = await homeDataAccess.GetHomeModelsById(id);

                if (output == null)
                {
                    return NotFound();
                }

                return Ok(output.FirstOrDefault());
            }
            catch (Exception ex)
            {

                Log.Information("Error by a GetHomeModelById Action: {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST api/UserController
        //Create Home Model for an existing user
        [HttpPost]
        public async Task<ActionResult<ReadHomeModelDTO>> CreateHome([FromBody] CreateHomeModelDTO createModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid User model");
                }

                var homeModel = mapper.Map<HomeModel>(createModel);

                await homeDataAccess.CreateHomeModel(createModel);

                var homeReadDTO = mapper.Map<ReadHomeModelDTO>(homeModel);

                return CreatedAtRoute(nameof(GetHomeModelById), new { Id = homeReadDTO.Id }, homeReadDTO);
            }
            catch (Exception ex)
            {
                Log.Information("Error by a CreateHome Action: {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT api/UserController/HomeModel
        [HttpPut]
        public async Task<ActionResult<ReadHomeModelDTO>> UpdateHomeModel(int id,[FromBody] HomeModel model)
        {
            try
            {
                var output = await homeDataAccess.GetHomeModelsById(id);

                if (output == null)
                {
                    return NotFound();
                }

                await homeDataAccess.UpdateHomeModel(model);

                var homeReadDTO = mapper.Map<ReadHomeModelDTO>(model);

                return CreatedAtRoute(nameof(GetHomeModelById), new { Id = homeReadDTO.Id }, homeReadDTO);
            }
            catch (Exception ex)
            {
                Log.Information("Error by a UpdateHomeModel Action: {ex}", ex);
                return StatusCode(500, "Internal server error");
            }

        }

        // DELETE api/HomeController/5
        [HttpDelete("{id}")]
        public ActionResult DeleteHome(int id)
        {
            try
            {
                var output = homeDataAccess.GetHomeModelsById(id);

                if (output == null)
                {
                    return NotFound();
                }

                homeDataAccess.DeleteHomeModel(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Information("Error by a DeleteHome Action: {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        
    }
}
