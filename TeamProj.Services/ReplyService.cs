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
            var userReply = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userReply.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if(!validId)
            {
                throw new Exception("Attempted tp build ReplyService without user Id claim");
            }

            _mapper = mapper;
            _context = dbContext;
        }

        public async Task<bool> CreateReplyAsync(ReplyModel request)
        {
            var replyEntity = _mapper.Map<ReplyModel, ReplyEntity>(request, opt =>
            opt.AfterMap((src, dest)=>dest.OwnerId = _userId));
            _context.Reply.Add(replyEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges ==1;
        }

        public async Task<IEnumerable<ReplyListItem>> GetAllRepliesAsync()
        {
            var replies = await _context.Reply
            .Where(entity => entity.OwnerId ==_userId)
            .Select(entity => _mapper.Map<ReplyListItem>(entity))
            .ToListAsync();

            return replies;
        }

        public async Task<ReplyDetail> GetReplyByIdAsync(int replyId)
        {

        var replyEntity = await _context.Reply
            .FirstOrDefaultAsync(e =>
                e.Id == replyId && e.OwnerId ==_userId);
        return replyEntity is null ? null : new ReplyDetail
        {
            Id = replyEntity.Id,
            Reply = replyEntity.Reply,
            CreatedUtc = replyEntity.CreatedUtc,
            ModifiedUtc = replyEntity.ModifiedUtc
        };
        }
        public async Task<bool> UpdateReplyAsync(ReplyUpdate request)
        {
            var replyEntity = await _context.Reply.FindAsync(request.Id);
            if(replyEntity?.OwnerId != _userId)
                return false;
            
            replyEntity.Reply = request.Reply;
            replyEntity.ModifiedUtc = DateTimeOffset.Now;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges ==1;
        }
        
        public async Task<bool> DeleteReplyAsync(int replyId)
        {
            var replyEntity = await _context.Reply.FindAsync(replyId);

            if (replyEntity?.OwnerId != _userId)
                return false;

            _context.Reply.Remove(replyEntity);
            return await _context.SaveChangesAsync() ==1;
        }
    }
