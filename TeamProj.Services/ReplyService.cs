using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ReplyService : IReplyService
    {
        private readonly IMapper _mapper;
        private readonly int _userId;
        private readonly ApplicationDbContext _context;
        public ReplyService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ApplicationDbContext dbContext)
        {
            // var userReply = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            // var value = userReply.FindFirst("Id")?.Value;
            // var validId = int.TryParse(value, out _userId);
            // if(!validId)
            // {
            //     throw new Exception("Attempted to build ReplyService without user Id claim");
            // }

            _mapper = mapper;
            _context = dbContext;
        }
        public async Task<bool> CreateReplyAsync(ReplyModel request)
        {
            var replyEntity = _mapper.Map<ReplyModel, ReplyEntity>(request);
            replyEntity.CreatedUtc = DateTime.Now;
            _context.Reply.Add(replyEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges ==1;
        }
        public async Task<IEnumerable<ReplyListItem>> GetAllRepliesAsync()
        {
            var reply = await _context.Reply.ToListAsync();
            var conversion = _mapper.Map<List<ReplyListItem>>(reply);
            return conversion;
        }
        public async Task<ReplyDetail> GetReplyByIdAsync(int replyId)
        {
        var replyEntity = await _context.Reply
            .FirstOrDefaultAsync(e =>
            e.Id == replyId);
        return replyEntity is null ? null : _mapper.Map<ReplyDetail>(replyEntity);
        }
        public async Task<bool> UpdateReplyAsync(int Id, ReplyUpdate request)
        {
            var replyEntity = await _context.Reply.FirstOrDefaultAsync(reply => 
            reply.Id == request.Id);
            if(replyEntity == null)
                return false;
                var newEntity = _mapper.Map(request, replyEntity);

                newEntity.ModifiedUtc = DateTime.Now;
                _context.Reply.Update(newEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges ==1;
        }     
        public async Task<bool> DeleteReplyAsync(int Id)
        {
            var replyEntity = await _context.Reply.FindAsync(Id);

            if (replyEntity == null)
                return false;

            _context.Reply.Remove(replyEntity);
            return await _context.SaveChangesAsync() ==1;
        }
    }
