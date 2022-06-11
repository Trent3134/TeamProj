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
            Text = request.Text
        };
        _dbContext.Posts.Add(postEntity);

        var numberOfChanges = await _dbContext.SaveChangesAsync();
        return numberOfChanges == 1;
    }

    public async Task<PostDetail> GetPostById(int postId)
    {
        var postEntity = await _dbContext.Posts
            .FirstOrDefault(e =>
                e.Id == postId && e.OwnerId == _userId
                );

        return PostEntity is null ? null : new PostDetail
        {
            Id = PostEntity.Id,
            Title = PostEntity.Title,
            Text = PostEntity.Text
        };
    }

    public async Task<PostUpdate> UpdatePostAsync(PostUpdate request)
    {
        var postEntity = await _dbContext.Posts.FindAsync(request.Id);

        if (postEntity?.OwnerId != _userId)
            return false;

        postEntity.Title = request.Title;
        postEntity.Text = request.Text;

        var numberOfChanges = await _dbContext.SaveChangesAsync();
        return numberOfChanges;
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        var postEntity = await _dbContext.Posts.FindPostAsync(postId);

        if (postEntity?.OwnerId != _userId)
            return false;

        _dbContext.Posts.Remove(postEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

}