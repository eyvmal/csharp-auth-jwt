using System.Security.Claims;
using exercise.wwwapi.Models;
using exercise.wwwapi.Payloads;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class BlogPostApi
{
    public static void ConfigureBlogPostApi(this WebApplication app)
    {
        var bp = app.MapGroup("blogpost");
        bp.MapGet("/", GetBlogposts);
        bp.MapGet("viewall/", ViewAllBlogPosts);
        bp.MapPost("new/", CreateNewBlogPost);
        bp.MapPut("edit/", EditBlogPost);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> GetBlogposts(IRepo<BlogPost> repo)
    {
        return Results.Ok(repo.GetAll());
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> ViewAllBlogPosts(IRepo<BlogPost> blogpostRepo, IRepo<Following> followRepo, ClaimsPrincipal user)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.Sid);
        if (userIdStr is null) return Results.Unauthorized();
        var userId = int.Parse(userIdStr);
        
        var followList = followRepo.GetAll().Where(f => f.User1Id == userId).ToList();
        var blogList = new List<BlogPost>();
        foreach (var followedUserBlogPosts in followList
                     .Select(follow => follow.User2Id)
                     .Select(followedUserId => blogpostRepo.GetAll()
                         .Where(bp => bp.AuthorId == followedUserId)
                         .ToList()))
        {
            blogList.AddRange(followedUserBlogPosts);
        }

        return Results.Ok(blogList);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> CreateNewBlogPost(IRepo<BlogPost> repo, ClaimsPrincipal user, PostBlogPostDto postBlogPost)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.Sid);
        if (userIdStr is null) return Results.Unauthorized();
        var userId = int.Parse(userIdStr);
        var bp = new BlogPost() { AuthorId = userId, Content = postBlogPost.Content };
        repo.Insert(bp);
        repo.Save();
        return Results.Ok(bp);
    }
    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    private static async Task<IResult> EditBlogPost(IRepo<BlogPost> repo, ClaimsPrincipal user, PostBlogPostDto postBlogPost)
    {
        var userIdStr = user.FindFirstValue(ClaimTypes.Sid);
        if (userIdStr is null) return Results.Unauthorized();
        var userId = int.Parse(userIdStr);
        var blogpost = repo.GetById(postBlogPost.Id);
        if (blogpost.AuthorId == userId) blogpost.Content = postBlogPost.Content;
        repo.Update(blogpost);
        return Results.Ok(blogpost);
    }
}