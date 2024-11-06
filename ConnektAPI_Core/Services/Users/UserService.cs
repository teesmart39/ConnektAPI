using ConnektAPI_Core.ApiModels.User;
using ConnektAPI_Core.Models;
using ConnektAPI_Core.Repository.UserRepo;

namespace ConnektAPI_Core.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepo userRepo;

    public UserService(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }


    public Task<OperationResult<List<UserProfileApiModel>>> GetUsers()
    {
        return userRepo.GetUsers();
    }

    public Task<OperationResult<UserProfileApiModel>> GetUserById(string id)
    {
        return userRepo.GetUserById(id);
    }
}