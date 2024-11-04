using ConnektAPI_Core.ApiModels.Auth.Login;
using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Models;

namespace ConnektAPI_Core.Services;

public interface IAuthService
{
    public Task<OperationResult<SignUpResponseModel>> SignUp(SignUpRequestModel signUpRequestModel);
    public Task<OperationResult<LoginResponseModel>> Login(LoginRequestModel loginRequestModel);
}