using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using exercise.wwwapi.Configurations;
using exercise.wwwapi.Models;
using exercise.wwwapi.Payloads;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace exercise.wwwapi.Endpoints;

public static class AuthApi
{
    public static void ConfigureAuthApi(this WebApplication app)
    {
        var auth = app.MapGroup("auth");
        auth.MapPost("register", Register);
        auth.MapPost("login", Login);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    private static async Task<IResult> Register(PostUserDto request, IRepo<User> service)
    {
        //user exists
        if (service.GetAll().Any(u => u.Username == request.Username))
            return Results.Conflict(new Payload<PostUserDto>() { status = "Username already exists!", data = request });
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new()
        {
            Username = request.Username,
            PasswordHash = passwordHash
        };

        service.Insert(user);
        service.Save();

        return Results.Ok(new Payload<string>() { data = "Created Account" });
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    private static async Task<IResult> Login(PostUserDto request, IRepo<User> service, ConfigurationSettings config)
    {
        //user doesn't exist
        if (!service.GetAll().Any(u => u.Username == request.Username)) return Results.BadRequest(new Payload<PostUserDto>() { status = "User does not exist", data = request });

        var user = service.GetAll().FirstOrDefault(u => u.Username == request.Username)!;

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Results.BadRequest(new Payload<PostUserDto>() { status = "Wrong Password", data = request });
        }
        var token =  CreateToken(user, config);
        return Results.Ok(new Payload<string>() { data =  token }) ;
       
    }
    private static string CreateToken(User user, ConfigurationSettings config)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)                
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue("AppSettings:Token")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}