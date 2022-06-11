[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    public PostController (IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreate request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (await _postService.CreatePostAsync(request))
            return Ok("Post was created successfully.");
        
        return BadRequest("Post could not be created.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

}