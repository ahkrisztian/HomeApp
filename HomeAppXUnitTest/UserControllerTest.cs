using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;
using HomeAppWebAPI.Controllers;
using HomeAppWebAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppXUnitTest
{
    public class UserControllerTest : IDisposable
    {
        Mock<IUserData> mockRepo;
        HomeProfiles realprofile;
        MapperConfiguration configuration;
        IMapper mapper;

        public UserControllerTest()
        {
            mockRepo = new Mock<IUserData>();
            realprofile = new HomeProfiles();
            configuration = new MapperConfiguration(config =>
            config.AddProfile(realprofile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realprofile = null;
        }

        [Fact]
        public void GetUserFullByObjectId_Returns404_NotFound()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetUserFullByObjectId(1)).Returns(() => null);
            var controller = new UserController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetUserFullByObjectId(1);

            //Assert

            Assert.IsType<NotFoundResult>(result.Result.Result);
        }

        [Fact]
        public void GetUserFullObjectId_Returns200_Ok()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetUserById(1)).Returns(Task.Run(() => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test",
                    ObjectId = "5"
                }
            }));

            var controller = new UserController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetUserFullByObjectId(1);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result.Result);
        }

        [Fact]
        public void CreateUser_ReturnsValidObject()
        {
            mockRepo.Setup(repo =>
            repo.GetUserById(1)).Returns(Task.Run(() => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test",
                    ObjectId = "5"
                }
            }));

            var controller = new UserController(mockRepo.Object, mapper);

            var result = controller.CreateUser(new CreateUserDTO { });

            Assert.IsType<ActionResult<ReadUserDTO>>(result.Result);
        }

        [Fact]
        public void CreateUser_Returns201_ValidObject()
        {
            mockRepo.Setup(repo =>
            repo.GetUserById(1)).Returns(Task.Run(() => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test",
                    ObjectId = "5"
                }
            }));

            var controller = new UserController(mockRepo.Object, mapper);

            var result = controller.CreateUser(new CreateUserDTO { });

            Assert.IsType<CreatedAtRouteResult>(result.Result.Result);
        }

        [Fact]
        public void UpdateHomeModel_Returns201_ValidObject()
        {
            mockRepo.Setup(repo =>
            repo.GetUserById(1)).Returns(Task.Run(() => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test",
                    ObjectId = "5"
                }
            }));

            var controller = new UserController(mockRepo.Object, mapper);

            var result = controller.UpdateUser(1, new UpdateUserDTO { });

            Assert.IsType<CreatedAtRouteResult>(result.Result.Result);
        }

        [Fact]
        public void DeleteUser_Returns_NoContent()
        {
            mockRepo.Setup(repo =>
            repo.GetUserById(1)).Returns(Task.Run(() => new List<UserModel>()
            {
                new UserModel()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "Test",
                    ObjectId = "5"
                }
            }));

            var controller = new UserController(mockRepo.Object, mapper);

            var result = controller.DeleteUser(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
