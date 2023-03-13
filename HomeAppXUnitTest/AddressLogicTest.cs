using AutoMapper;
using HomeAppDataAccessLibrary.DataAccess.AddressDataAccess;
using HomeAppDataAccessLibrary.Models.AddressModels;
using HomeAppDataAccessLibrary.Models.DTOs.AddressDTO;
using HomeAppWebAPI.Profiles;
using HomeAppXUnitTest.MockDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAppXUnitTest
{
    public class AddressLogicTest
    {
        HomeProfiles realprofile;
        MapperConfiguration configuration;
        IMapper mapper;

        public AddressLogicTest()
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
        public void AddAddressByUserIdTest()
        {
            MockDataAccessContextAddress da = new MockDataAccessContextAddress(mapper);

            da.AddAddressByUserId(new ReadAddressDTO()
            {
                Id = 3,
                Country = "USA",
                City = "Houston",
                Street = "Watt Str. 22",
                UserId = 1
            }).Wait();

            int expected = 3;
            var actual = da.users.Where(i => i.Id == 1).FirstOrDefault().Addresses.Where(i => i.Id == 3).FirstOrDefault();

            Assert.Equal(expected, actual.Id);

            string expectedCity = "Houston";
            var actualCity = da.users.Where(i => i.Id == 1).FirstOrDefault().Addresses.Where(i => i.Id == 3).FirstOrDefault();

            Assert.Equal(expectedCity, actualCity.City);
        }

        [Fact]
        public void DeleteAddressTest()
        {
            MockDataAccessContextAddress da = new MockDataAccessContextAddress(mapper);

            da.DeleteAddressByUserId(1).Wait();

            int expected = 0;

            int actual = da.users.Where(i => i.Id == 1).FirstOrDefault().Addresses.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAddressesByUserIdTest()
        {
            MockDataAccessContextAddress da = new MockDataAccessContextAddress(mapper);

            var output = da.GetAddressesByUserId(1).Result;

            int expected = 1;

            int actual = output.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateAddress()
        {
            MockDataAccessContextAddress da = new MockDataAccessContextAddress(mapper);

            var output = da.UpdateUser(new AddressModel
            {
                Id = 1,
                Country = "France",
                City = "Paris",
                Street = "Gaul Ave. 2"   ,
                UserId = 1
            }).Result;

            string expected = "France";

            string actual = output.Where(i => i.Id == 1).FirstOrDefault().Country;

            Assert.Equal(expected, actual);
        }

    }


}
