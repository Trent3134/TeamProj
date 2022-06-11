using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class ReplyModel
    {
        [Key]
        public int Id { get; set; } 
        public int OwnerId { get; set; }
        [Required]
        [MinLength(1, ErrorMessage ="{0} must be at least {1} characters long.")]
        [MaxLength(500, ErrorMessage = "{0} mus contain no more than {1} characters.")]
        public string Reply { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
