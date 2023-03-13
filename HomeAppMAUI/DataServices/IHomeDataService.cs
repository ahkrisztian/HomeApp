using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;
using HomeAppMAUI.Model;

namespace HomeAppMAUI.DataServices
{
    public interface IHomeDataService
    {
        Task<HomeModel> CreateHomeModel(NewHomeModel createModel);
        Task DeleteHomeModel(int id);
        Task<List<HomeModel>> GetHomeModelsById(int id);
        Task<HomeModel> UpdateHomeModel(HomeModel model);
    }
}