using AutoMapper;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.UserModels;
using HomeAppWebAPI.Profiles;
using HomeAppXUnitTest.MockDataAccess;

namespace HomeAppXUnitTest
{
    public class UserLogicTest : IDisposable
    {
        HomeProfiles realprofile;
        MapperConfiguration configuration;
        IMapper mapper;

        public UserLogicTest()
        {
            realprofile = new HomeProfiles();
            configuration = new MapperConfiguration(config =>
            config.AddProfile(realprofile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mapper = null;
            configuration = null;
            realprofile = null;
        }

        [Fact]
        public void GetUserById_ReturnsaUser()
        {
            MockDataAccessContext da = new MockDataAccessContext(mapper);

            UserModel expected = new UserModel()
            {
                Id = 1,
                Name = "Tom Jones",
                Email = "jones@tom.com",
                ObjectId = "1"
            };

            var actual = da.GetUserById(1);

            Assert.Equal(expected.Name, actual.Result.Name);
        }

        [Fact]

        public void DeleteUserTest()
        {
            MockDataAccessContext da = new MockDataAccessContext(mapper);

            da.DeleteUser(1).Wait();

            int expected = 1;

            int actual = da.users.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateUserTest()
        {
            MockDataAccessContext da = new MockDataAccessContext(mapper);

            CreateUserDTO newUser = new CreateUserDTO()
            {
                Name = "Ed Williams",
                Email = "williams@ed.com",
                ObjectId = "3"
            };

            var output = da.CreateUser(newUser);

            string expectedName = "Ed Williams";
            string actualName = output.Result.Name;

            Assert.Equal(expectedName, actualName);

            int expected = 3;
            int actual = da.users.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUserByObjectIdTest()
        {
            MockDataAccessContext da = new MockDataAccessContext(mapper);

            var output = da.GetUserByObjectId("1");

            int expected = 1;

            int actual = output.Result.Id;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateUserTest()
        {
            MockDataAccessContext da = new MockDataAccessContext(mapper);

            var output = da.UpdateUser(new UserModel()
            {
                Id = 2,
                Name = "Billy Bob",
                Email = "billy@bob.com",
                ObjectId = "2"
            });

            var expected = "Billy Bob";

            output.Wait();

            var actual = output.Result.Name;

            Assert.Equal(expected, actual);
        }
    }
}
