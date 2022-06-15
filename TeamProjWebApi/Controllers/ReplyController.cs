using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


    [Route("api/[controller]")] 
    [ApiController]
    public class ReplyController : ControllerBase
    {   
        private readonly IReplyService _rservice;
        public ReplyController(IReplyService rservice)
        {
            _rservice = rservice;
        }
        [HttpPost]
        public async Task<IActionResult> ModelReply([FromBody] ReplyModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _rservice.CreateReplyAsync(request))
                return Ok("Reply created successfully.");

            return BadRequest("Reply could not be created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReplies()
        {
            var replies = await _rservice.GetAllRepliesAsync();
            return Ok(replies);
        }

        [HttpGet("{replyId:int}")]
        public async Task<IActionResult> GetReplyById([FromRoute] int replyId)
        {
            var detail = await _rservice.GetReplyByIdAsync(replyId);
            return detail is not null
                ?Ok(detail)
                : NotFound();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateReplyAsync([FromRoute] int Id, ReplyUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _rservice.UpdateReplyAsync(Id, request)
                ? Ok("Reply updated.")
                : BadRequest("Could not be updated.");
        }

        [HttpDelete("{replyId:int}")]
        public async Task<IActionResult> DeleteReplyAsync([FromRoute] int replyId)
        {
            return await _rservice.DeleteReplyAsync(replyId)
                ? Ok($"Reply {replyId} was deleted.")
                : BadRequest($"Reply {replyId} could not be deleted.");
        }
    }