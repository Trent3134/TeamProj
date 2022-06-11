public interface IPostService
{
    Task<IEnumerable<PostListItem>> GetAllPostsAsync();
}