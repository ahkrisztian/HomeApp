using HomeAppDataAccessLibrary.Models.DTOs.HomeModelDTO;
using HomeAppDataAccessLibrary.Models.HomeModels;

namespace HomeAppDataAccessLibrary.DataAccess.HomeModelDataAccess
{
    public interface IHomeDataAccess
    {
        Task<List<HomeModel>> GetHomeModelsById(int id);
        Task<HomeModel> CreateHomeModel(CreateHomeModelDTO createModel);

        Task<HomeModel> UpdateHomeModel(HomeModel model);

        Task DeleteHomeModel(int id);
    }
}