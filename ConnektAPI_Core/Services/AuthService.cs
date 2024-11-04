using ConnektAPI_Core.ApiModels.Auth.Login;
using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Models;
using ConnektAPI_Core.Repository.Auth;

namespace ConnektAPI_Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuth auth;

    public AuthService(IAuth auth)
    {
        this.auth = auth;
    }
    public Task<OperationResult<SignUpResponseModel>> SignUp(SignUpRequestModel signUpRequestModel)
    {
        return auth.SignUp(signUpRequestModel);
    }

    public Task<OperationResult<LoginResponseModel>> Login(LoginRequestModel loginRequestModel)
    {
        throw new NotImplementedException();
    }
}