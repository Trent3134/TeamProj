using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public interface IReplyService
    {
        Task<IEnumerable<ReplyListItem>> GetAllRepliesAsync();
        Task<bool> CreateReplyAsync(ReplyCreate request);
        Task<ReplyDetail> GetReplyByIdAsync(int replyId);
        Task<bool> UpdateReplyAsync(ReplyUpdate request);
        Task<bool> DeleteReplyAsync(int replyId);

    }
