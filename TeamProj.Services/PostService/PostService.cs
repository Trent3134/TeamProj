using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


public class PostService : IPostService
{
    private readonly int _userId;
    private readonly ApplicationDbContext _dbContext;
    public PostService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
    {
        var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var value = userClaims.FindFirst("Id")?.Value;

        var validId = int.TryParse(value, out _userId);
        if (!validId)
        {
            throw new Exception("Attempted to build PostService without User Id claim");
        }
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<PostListItem>> GetAllPostsAsync()
    {
        var posts = await _dbContext.Posts
            .Where(entity => entity.OwnerId == _userId)
            .Select(entity => new PostListItem
            {
                Id = entity.Id,
                Title = entity.Title,
            })
        .ToListAsync();

        return posts;
    }

    public async Task<bool> CreatePostAsync(PostCreate request)
    {
        var postEntity = new PostEntity
        {
            Title = request.Title,
            Text = request.Text,
            CreatedUtc = DateTime.Now,
            OwnerId = _userId
        };
        _dbContext.Posts.Add(postEntity);

        var numberOfChanges = await _dbContext.SaveChangesAsync();
        return numberOfChanges == 1;
    }

    public async Task<PostDetail> GetPostByIdAsync(int postId)
    {
        var postEntity = await _dbContext.Posts
            .FirstOrDefaultAsync(e =>
                e.Id == postId && e.OwnerId == _userId
                );

        return postEntity is null ? null : new PostDetail
        {
            Id = postEntity.Id,
            Title = postEntity.Title,
            Text = postEntity.Text
        };
    }

    public async Task<bool> UpdatePostAsync(PostUpdate request)
    {
        var postEntity = await _dbContext.Posts.FindAsync(request.Id);

        if (postEntity?.OwnerId != _userId)
            return false;

        postEntity.Title = request.Title;
        postEntity.Text = request.Text;

        var numberOfChanges = await _dbContext.SaveChangesAsync();
        return numberOfChanges == 1;
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        var postEntity = await _dbContext.Posts.FindAsync(postId);

        if (postEntity?.OwnerId != _userId)
            return false;

        _dbContext.Posts.Remove(postEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

}