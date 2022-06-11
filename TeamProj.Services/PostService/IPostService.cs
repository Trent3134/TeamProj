public interface IPostService
{
    Task<bool> CreatePostAsync(PostCreate request);
    Task<IEnumerable<PostListItem>> GetAllPostsAsync();
    Task<PostDetail> GetPostByIdAsync (int postId);
    Task<bool> UpdatePostAsync(PostUpdate request);
}