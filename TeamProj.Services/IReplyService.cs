using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public interface IReplyService
    {
        Task<IEnumerable<ReplyListItem>> GetAllRepliesAsync();
        Task<bool> CreateReplyAsync(ReplyModel request);
        Task<ReplyDetail> GetReplyByIdAsync(int Id);
        Task<bool> UpdateReplyAsync(int Id, ReplyUpdate request);
        Task<bool> DeleteReplyAsync(int Id);
    }
