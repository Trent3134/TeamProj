using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public interface ICommentService
    {
        Task<bool> CreateCommentAsync(CommentCreate req);
        Task<IEnumerable<CommentListItem>> GetAllCommentsAsync();
        Task<CommentsUpdate> GetCommentByIdAsync();
        Task<bool> UpdateCommentAsync(CommentsUpdate req);
        Task<bool> DeleteCommentAsync(int CommentId);
    }
