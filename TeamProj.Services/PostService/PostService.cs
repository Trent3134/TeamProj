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
}