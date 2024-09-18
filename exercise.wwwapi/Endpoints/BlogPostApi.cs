namespace exercise.wwwapi.Endpoints;

public static class BlogPostApi
{
    public static void ConfigureBlogPostApi(this WebApplication app)
    {
        var blog = app.MapGroup("blog");
    }
}