using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class UserApi
{
    public static void ConfigureUserApi(this WebApplication app)
    {
        var users = app.MapGroup("user");
        users.MapGet("/", GetUsers);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> GetUsers(IRepo<User> service)
    {
        return Results.Ok(service.GetAll());
    }
}