using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAppMAUI.DataServices
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public UserDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5209" : "https://localhost:7068";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<ReadUserDTO> GetUserById(int id)
        {
            ReadUserDTO user = new ReadUserDTO();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet access");
                return user;
            }

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{_url}/User/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    user = JsonSerializer.Deserialize<ReadUserDTO>(content, _jsonSerializerOptions);

                }
                else
                {
                    Debug.WriteLine("No Response");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception {ex}", ex.Message);
            }

            return user;
        }
    }
}
