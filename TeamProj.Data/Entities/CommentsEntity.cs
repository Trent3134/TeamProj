using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


    public class CommentsEntity
    {
        [Key]
         public int Id { get; set; }
         [Required]
         [ForeignKey(nameof(Account))]
         public int AccountId { get; set; }
         public UserEntity Account { get; set; }
         [Required]
         public string Text { get; set; }
         [Required]
         public DateTimeOffset CreatedAt { get; set; }
         [Required]
         public DateTimeOffset? ModifiedAt { get; set; }
         //public virtual List<RepliesEntity> Replies  { get; set; }
    }
