using ConnektAPI_Core.ApiModels.User;
using ConnektAPI_Core.Models;

namespace ConnektAPI_Core.Services.Users;

public interface IUserService
{
    public Task<OperationResult<List<UserProfileApiModel>>> GetUsers();
    public Task<OperationResult<UserProfileApiModel>> GetUserById(string id);
}