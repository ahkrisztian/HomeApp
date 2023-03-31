using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace HomeAppMAUI.DataServices
{
    public class HomeDataService : IHomeDataService
    {
        private readonly HttpClient httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public HomeDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5209" : "https://localhost:7068";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<HomeModel> CreateHomeModel(CreateHomeModelDTO createModel)
        {
            HomeModel newHomeModel = null;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet access");
                return null;
            }

            try
            {
                string home = JsonSerializer.Serialize<CreateHomeModelDTO>(createModel, _jsonSerializerOptions);

                StringContent content = new StringContent(home, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync($"{_url}/Home", content);

                if (response.IsSuccessStatusCode)
                {
                    string newHome = await response.Content.ReadAsStringAsync();

                    newHomeModel = JsonSerializer.Deserialize<HomeModel>(newHome);
                    Debug.WriteLine("Success");
                }
                else
                {
                    Debug.WriteLine("No Response");
                    return newHomeModel;
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception {ex}", ex.Message);
            }

            return newHomeModel;
        }

        public async Task DeleteHomeModel(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet access");
                return;
            }

            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{_url}/Home/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success");
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

            return;
        }

        public async Task<List<HomeModel>> GetHomeModelsById(int id)
        {
            List<HomeModel> homes = new List<HomeModel>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet access");
                return homes;
            }

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{_url}/Home/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    homes = JsonSerializer.Deserialize<List<HomeModel>>(content, _jsonSerializerOptions);

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

            return homes;
        }

        public async Task<HomeModel> UpdateHomeModel(HomeModel model)
        {
            HomeModel newHomeModel = null;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No Internet access");
                return newHomeModel;
            }

            try
            {
                string home = JsonSerializer.Serialize<HomeModel>(model, _jsonSerializerOptions);

                StringContent content = new StringContent(home, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync($"{_url}/Home/{model.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    string newHome = await response.Content.ReadAsStringAsync();

                    newHomeModel = JsonSerializer.Deserialize<HomeModel>(newHome);
                    Debug.WriteLine("Success");
                }
                else
                {
                    Debug.WriteLine("No Response");
                    return newHomeModel;
                }


            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception {ex}", ex.Message);
            }

            return newHomeModel;
        }
    }
}
