using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _cservice;
    public CommentController(ICommentService commentService)
    {
        _cservice = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommentCreate req)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        if (await _cservice.CreateCommentAsync(req))
        {
            return Ok("Comment was successfully created");
        }
        return BadRequest("comment could not be created.");
    }

    [HttpGet]
    //[ProducesResponseType(typeof(IEnumerable<CommentListItem>), 200)]
    public async Task<IActionResult> GetAllCommentsAsync()
    {
        var comments = await  _cservice.GetAllCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("{commentId:int}")]
   // [ProducesResponseType(typeof(IEnumerable<CommentDetail>), 200)]
    public async Task<IActionResult> GetCommentById([FromRoute] int commentId)
    {
        var cDetail = await _cservice.GetCommentByIdAsync(commentId);
        return cDetail is not null ? Ok(cDetail) : NotFound();
    }

    [HttpPut]
   // [ProducesResponseType(typeof(IEnumerable<bool>), 200)]
    public async Task<IActionResult> UpdateCommentById([FromBody] CommentsUpdate req)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
         return await _cservice.UpdateCommentAsync(req)? Ok($"comment  was Updated successfully"): BadRequest($"comment  could not be Updated.");
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        return await _cservice.DeleteCommentAsync(commentId)
        ? Ok($"Comment {commentId} was deleted successfully!") : BadRequest($"Comment {commentId} could not be deleted");
    }
}
