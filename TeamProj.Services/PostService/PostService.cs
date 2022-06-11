public class PostService : IPostService
{
    private readonly int _userId;
    public PostService(IHttpContextAccessor httpContextAccessor)
    {
        var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var value = userClaims.FindFirst("Id")?.Value;

        var validId = int.TryParse(value, out _userId);
        if (!validId)
        {
            throw new Exception("Attempted to build PostService without User Id claim");
        }
    }
}