using System.Security.Claims;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class UserApi
{
    public static void ConfigureUserApi(this WebApplication app)
    {
        var user = app.MapGroup("user");
        user.MapGet("/", GetUsers);
        user.MapGet("{id}", GetUserById);
        user.MapPost("follow/{id}", FollowUser);
        user.MapPost("unfollow/{id}", UnfollowUser);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> GetUsers(IRepo<User> repo)
    {
        return Results.Ok(repo.GetAll());
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> GetUserById(IRepo<User> repo, int id)
    {
        return Results.Ok(repo.GetById(id));
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> FollowUser(IRepo<Following> repo, int id, ClaimsPrincipal user)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.Sid);
        if (userIdStr is null) return Results.Unauthorized();
        var userId = int.Parse(userIdStr);
        Following x = new() { User1Id = userId, User2Id = id };
        repo.Insert(x);
        repo.Save();
        return Results.Ok(x);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> UnfollowUser(IRepo<Following> repo, int id, ClaimsPrincipal user)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.Sid);
        if (userIdStr is null) return Results.Unauthorized();
        var userId = int.Parse(userIdStr);
        var x = repo.GetAll().FirstOrDefault(f => f.User1Id == userId && f.User2Id == id)!;
        repo.Delete(x.Id);
        repo.Save();
        return Results.Ok(x);
    }
}