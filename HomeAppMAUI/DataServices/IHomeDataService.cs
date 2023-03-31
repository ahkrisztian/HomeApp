using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;

namespace HomeAppMAUI.DataServices
{
    public interface IHomeDataService
    {
        Task<HomeModel> CreateHomeModel(CreateHomeModelDTO createModel);
        Task DeleteHomeModel(int id);
        Task<List<HomeModel>> GetHomeModelsById(int id);
        Task<HomeModel> UpdateHomeModel(HomeModel model);
    }
}