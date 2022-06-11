using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ReplyEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [ForeignKey(nameof(Com))]
        public int CommentId { get; set; }
        public CommentsEntity Com { get; set; }
        public int OwnerId { get; set; }
        public string Reply { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

       // public virtual list<CommentsEntity>Comment { get; set; }

    }
