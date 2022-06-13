using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



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

    [HttpGet("{noteId:int}")]
    public async Task<IActionResult> GetPostById([FromRoute] int postId)
    {
        var detail = await _postService.GetPostByIdAsync(postId);

        return detail is not null
            ? Ok(detail)
            : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePostById([FromBody] PostUpdate request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            return await _postService.UpdatePostAsync(request)
                ? Ok("Post Updated Successfully.")
                : BadRequest("Post Could Not Be Updated.");
    }

    [HttpDelete("{postId:int}")]
    public async Task<IActionResult> DeletePostFromId([FromRoute] int postId)
    {
        return await _postService.DeletePostAsync(postId)
            ? Ok("Post was deleted successfully.")
            : BadRequest("Post could not be deleted.");
    }


}