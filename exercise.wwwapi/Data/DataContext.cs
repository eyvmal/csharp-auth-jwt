using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>().Navigation(x => x.Author).AutoInclude();
        modelBuilder.Entity<BlogPost>().Navigation(x => x.Comments).AutoInclude();
        modelBuilder.Entity<Comment>().Navigation(x => x.Author).AutoInclude();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Following> FollowUsers { get; set; }
}