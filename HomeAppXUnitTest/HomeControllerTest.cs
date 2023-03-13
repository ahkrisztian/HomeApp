using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess;
using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppDataAccessLibrary.Models.RoomModels;
using HomeAppWebAPI.Controllers;
using HomeAppWebAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeAppXUnitTest
{
    public class HomeControllerTest : IDisposable
    {
        Mock<IHomeDataAccess> mockRepo;
        HomeProfiles realprofile;
        MapperConfiguration configuration;
        IMapper mapper;

        public HomeControllerTest()
        {
            mockRepo = new Mock<IHomeDataAccess>();
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
        public void DeleteModelTestRetunsNoContent()
        {
            //Arrange

            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(1)).Returns(Task.Run(() => new List<HomeModel>() 
            {
                new HomeModel
                {

                    Id = 1,
                    Name = "Home1",
                    UserId = 1,
                    AddressId = 1,
                    Description = "Home1"
                }
            }));
            

            var controller = new HomeController(mockRepo.Object, mapper);

            //Act
            var result = controller.DeleteHome(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void DeleteModelTestReturnsBadRequst()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(0)).Returns(() => null);

            var controller = new HomeController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteHome(0);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]

        public void GetHomeModelTestReturnsNotFound()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(0)).Returns(() => null);

            var controller = new HomeController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetHomeModelById(1);
            //Assert
            Assert.IsType<NotFoundResult>(result.Result.Result);
        }

        [Fact]
        public void GetHomeModelTestReturnsOk()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(1)).Returns(Task.Run(() => new List<HomeModel>()
            {
                new HomeModel
            {
                Id = 1,
                Name = "Home1",
                UserId = 1,
                AddressId = 1,
                Description = "Home1"
            }
            }));

            var controller = new HomeController(mockRepo.Object, mapper);

            //Act
            var result = controller.GetHomeModelById(1);

            //Assert
            Assert.IsType<ActionResult<List<HomeModel>>>(result.Result);
        }

        [Fact]
        public void CreateHomeModelTestRetunrsCorrectType()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(1)).Returns(Task.Run(() => new List<HomeModel>()
            {
                new HomeModel
                {
                    Id = 1,
                    Name = "Home1",
                    UserId = 1,
                    AddressId = 1,
                    Description = "Home1"
                }
            }));

            var controller = new HomeController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateHome(new CreateHomeModelDTO { });

            //Assert
            Assert.IsType<ActionResult<ReadHomeModelDTO>>(result.Result);
        }

        [Fact]
        public void CreateHomeModelTestReturns201_WhenValidObjSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(1)).Returns(Task.Run(() => new List<HomeModel>()
            {
                new HomeModel
                {
                    Id = 1,
                    Name = "Home1",
                    UserId = 1,
                    AddressId = 1,
                    Description = "Home1"
                }
            }));

            var controller = new HomeController(mockRepo.Object, mapper);

            //Act
            var result = controller.CreateHome(new CreateHomeModelDTO { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result.Result);
        }

        [Fact]
        public void UpdateHomeModelTestReturns204NoContent_WhenValidObject()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetHomeModelsById(1)).Returns(Task.Run(() => new List<HomeModel>()
            {
                new HomeModel
                {
                    Id = 1,
                    Name = "Home1",
                    UserId = 1,
                    AddressId = 1,
                    Description = "Home1"
                }
            }));
            var controller = new HomeController(mockRepo.Object, mapper);
            //Act

            var result = controller.UpdateHomeModel(1, new HomeModel 
            {
                Id = 1,
                Name = "Home2",
                UserId = 1,
                AddressId = 1,
                Description = "Home2",
                Kitchen = new List<KitchenModel> 
                { new KitchenModel
                    {
                        Description = "New",
                        HomeModelId = 1,
                        RoomTypeId = 1
                    } 
                }
            });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result.Result);           
        }
    }
}
