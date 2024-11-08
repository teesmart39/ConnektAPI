using System.Net;
using ConnektAPI_Core.ApiModels.Photos;
using ConnektAPI_Core.ApiModels.User;
using ConnektAPI_Core.Entities;
using ConnektAPI_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConnektAPI_Core.Repository.UserRepo;

public class UserRepo : IUserRepo
{
    private readonly ILogger<UserRepo> logger;
    private readonly UserManager<ApplicationUser> userManager;

    public UserRepo(UserManager<ApplicationUser> userManager, ILogger<UserRepo> logger)
    {
        this.userManager = userManager;
        this.logger = logger;
    }


    public async Task<OperationResult<List<UserProfileApiModel>>> GetUsers()
    {
        try
        {
            //var result = new OperationResult<ApplicationUser>();

            var users = await userManager.Users
                .Include(x => x.Photos)
                .Include(x => x.Likes)
                .Include(x => x.Posts)
                .Include(x => x.Following)
                .Include(x => x.Followers)
                .Select(x => new UserProfileApiModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Nationality = x.Nationality,
                    Photos = x.Photos.Select(photo => new PhotosApiModel
                    {
                        PhotoId = photo.PhotoId,
                        PhotoUrl = photo.PhotoUrl
                    }).ToList()
                }).ToListAsync();

            return new OperationResult<List<UserProfileApiModel>> { Result = users };
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new OperationResult<List<UserProfileApiModel>>
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorTitle = "SYSTEM ERROR",
                ErrorMessage = "An error occurred while getting the users."
            };
        }
    }

    public async Task<OperationResult<UserProfileApiModel>> GetUserById(string id)
    {
        try
        {
            var user = await userManager.Users
                .Include(x => x.Photos)
                .Include(x => x.Likes)
                .Include(x => x.Posts)
                .Include(x => x.Following)
                .Include(x => x.Followers)
                .Select(x => new UserProfileApiModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Nationality = x.Nationality,
                    Photos = x.Photos.Select(photo => new PhotosApiModel
                    {
                        PhotoId = photo.PhotoId,
                        PhotoUrl = photo.PhotoUrl
                    }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == id);


            if (user == null)
                return new OperationResult<UserProfileApiModel>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorTitle = "NOT FOUND",
                    ErrorMessage = "The specified user does not exist."
                };
            return new OperationResult<UserProfileApiModel>
            {
                Result = user
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new OperationResult<UserProfileApiModel>
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorTitle = "SYSTEM ERROR",
                ErrorMessage = "An error occurred while getting the users."
            };
        }
    }

    public async Task<OperationResult> UpdateUserProfile(UpdateUserProfileApiModel credentials, string id)
    {
        try
        {
            var user  = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new OperationResult
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorTitle = "NOT FOUND",
                    ErrorMessage = "The specified user does not exist."
                };
            }
            user.FirstName = credentials.FirstName ?? user.FirstName;
            user.LastName = credentials.LastName ?? user.LastName;
            user.Nationality = credentials.Nationality;
            user.Email = credentials.Email ?? user.Email;
            user.PhoneNumber = credentials.PhoneNumber ?? user.PhoneNumber;
            user.DateOfBirth = credentials.DateOfBirth ?? user.DateOfBirth;
            
           await  userManager.UpdateAsync(user);

           return new OperationResult
           {
               StatusCode = (int)HttpStatusCode.OK,

           };
        }
        catch (Exception e)
        {
            return new OperationResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorTitle = "SYSTEM ERROR",
                ErrorMessage = e.Message
            };
        }
    }
}