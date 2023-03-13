using HomeAppDataAccessLibrary.DataAccess.UserDataAccess;
using HomeAppDataAccessLibrary.Models.DTOs.UserDTO;
using Microsoft.AspNetCore.Components.Authorization;

namespace HomeAppWebAPI.Data
{
    public static class AuthenticationStateProviderHelper
    {
        public static async Task<ReadUserDTO> GetUserFromAuth(
                            this AuthenticationStateProvider provider,
                            IUserData userData)
        {
            var authState = await provider.GetAuthenticationStateAsync();
            string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
            return await userData.GetUserByObjectId(objectId);
        }
    }
}
