using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ConnektAPI_Core.ApiModels.Auth.Login;
using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Data;
using ConnektAPI_Core.Entities;
using ConnektAPI_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ConnektAPI_Core.Repository.Auth;

public class Auth : IAuth
{
    private readonly IConfiguration configuration;
    private readonly ApplicationDbContext context;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly UserManager<ApplicationUser> userManager;

    public Auth(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.configuration = configuration;
    }

    public async Task<OperationResult<SignUpResponseModel>> SignUp(SignUpRequestModel signUpRequestModel)
    {
        var user = new ApplicationUser
        {
            UserName = signUpRequestModel.Username,
            Email = signUpRequestModel.Email
        };

        // Create the user using the UserManager
        var result = await userManager.CreateAsync(user, signUpRequestModel.Password);

        // Check if the creation was successful
        if (result.Succeeded)
        {
            var response = new SignUpResponseModel
            {
                UserId = user.Id
            };

            return new OperationResult<SignUpResponseModel>
            {
                Result = response
                //IsSuccess = true,
            };
        }

        // Return an error result if creation failed
        return new OperationResult<SignUpResponseModel>
        {
            ErrorTitle = "Sign Up Failed",
            ErrorMessage = "Failed to sign up",
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }

// Method to generate the JWT token (simplified example)
    // private string GenerateJwtToken(ApplicationUser user)
    // {
    //     // Your JWT token generation logic here
    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var key = Encoding.ASCII.GetBytes("YourSecretKeyHere"); // Replace with your secret key
    //     var tokenDescriptor = new SecurityTokenDescriptor
    //     {
    //         Subject = new ClaimsIdentity(new[]
    //         {
    //             new Claim(ClaimTypes.Name, user.UserName),
    //             new Claim(ClaimTypes.NameIdentifier, user.Id)
    //         }),
    //         Expires = DateTime.UtcNow.AddDays(7),
    //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //     };
    //
    //     var token = tokenHandler.CreateToken(tokenDescriptor);
    //     return tokenHandler.WriteToken(token);
    // }


    public async Task<OperationResult<LoginResponseModel>> Login(LoginRequestModel loginRequestModel)
    {
        try
        {
            var user = await userManager.FindByNameAsync(loginRequestModel.Username);
            if (user == null)
                return new OperationResult<LoginResponseModel>
                {
                    ErrorTitle = "User Not Found",
                    ErrorMessage = "Invalid username"
                };

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestModel.Password);

            if (!checkPasswordResult)
                return new OperationResult<LoginResponseModel>
                {
                    ErrorTitle = "Incorrect Password",
                    ErrorMessage = "Invalid username"
                };

            var signingCredential =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])),
                    SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            var jwtObject = new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                null,
                DateTime.Now.AddHours(1),
                signingCredential
            );

            var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

            return new OperationResult<LoginResponseModel>
            {
                Result = new LoginResponseModel { Token = jwtString }
            };
        }
        catch (Exception ex)
        {
            return new OperationResult<LoginResponseModel>
            {
                ErrorTitle = ex.Message,
                ErrorMessage = ex.StackTrace
            };
        }
    }
}