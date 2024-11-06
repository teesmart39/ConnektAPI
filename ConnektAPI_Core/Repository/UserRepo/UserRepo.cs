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
}