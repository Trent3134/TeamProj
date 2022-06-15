using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeamProj.Models.Comments;

public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly int _userId;
        private readonly ApplicationDbContext _context;
        public CommentService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ApplicationDbContext dbContext)
        {
            var userComments = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userComments.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
            {
                throw new Exception("attemped to build CommentService without user id claim");
            }

        _mapper = mapper;
        _context = dbContext;
        }
        
        public async Task<bool> CreateCommentAsync(CommentCreate req)
        {
            var commentEntity = _mapper.Map<CommentCreate, CommentsEntity>(req, opt => 
            opt.AfterMap((src, dest) => dest.AccountId = _userId)
            );
            _context.Comment.Add(commentEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<CommentListItem>> GetAllCommentsAsync()
        {
            var comments = await _context.Comment
            .Where(entity => entity.AccountId == _userId)
            .Select(entity => _mapper.Map<CommentListItem>(entity))
            .ToListAsync();

            return comments;
        }

        public async Task<CommentDetail> GetCommentByIdAsync(int Id)
        {
            var commentEntity = await _context.Comment
            .FirstOrDefaultAsync(e => 
            e.Id == Id && e.AccountId == _userId);
            return commentEntity is null ? null: _mapper.Map<CommentDetail>(commentEntity);
        }


        public async Task<bool> UpdateCommentAsync(CommentsUpdate req)
        {
            var commentIsOwned = await _context.Comment.AnyAsync(comment =>
            comment.Id == req.Id && comment.AccountId == _userId);

            if (commentIsOwned)
            {
                return false;
            }
            var newEntity = _mapper.Map<CommentsUpdate, CommentsEntity>(req, opt => 
            opt.AfterMap((src, dest) => dest.AccountId = _userId)
            );
            _context.Entry(newEntity).State = EntityState.Modified;
            _context.Entry(newEntity).Property(e => e.CreatedAt).IsModified = false;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges ==1;
        }

        public async Task<bool> DeleteCommentAsync(int Id)
        {
            var commentEntity = await _context.Comment.FindAsync(Id);
            if (commentEntity?.AccountId != _userId)
            {
                return false;
            }
            _context.Comment.Remove(commentEntity);
            return await _context.SaveChangesAsync() == 1;
        }
        
    }
