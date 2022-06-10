using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



    public class ReplyEntity : CommentEntity
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [MinLength(1, ErrorMessage ="{0{ must be at least {1} characters long.")]
        [MaxLength(500, ErrorMessage = "{0} mus contain no more than {1} characters.")]
        public string Reply { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
    }
