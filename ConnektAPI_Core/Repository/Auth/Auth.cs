using ConnektAPI_Core.ApiModels.Auth.Login;
using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Data;
using ConnektAPI_Core.Entities;
using ConnektAPI_Core.Models;
using Microsoft.AspNetCore.Identity;

namespace ConnektAPI_Core.Repository.Auth;

public class Auth : IAuth
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<User> userManager;

    public Auth(ApplicationDbContext context, UserManager<User> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }
    public Task<OperationResult<SignUpResponseModel>> SignUp(SignUpRequestModel signUpRequestModel)
    {
        // var user = new User
        // {
        //     UserName = signUpRequestModel.Username,
        //     Email = signUpRequestModel.Email,
        //    // PasswordHash = signUpRequestModel.Password,
        // };
        //
        // userManager.CreateAsync(user, signUpRequestModel.Password);
        // return new Task<OperationResult<SignUpResponseModel>>(
        // {
        //     
        // })
        throw new NotImplementedException();
    }

    public Task<OperationResult<LoginResponseModel>> Login(LoginRequestModel loginRequestModel)
    {
        throw new NotImplementedException();
    }
}