using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class CommentCreate
    {
        [MinLength(1, ErrorMessage = "{0} must be at least {1} character long.")]
        [MaxLength(100, ErrorMessage = "{0} must be at least {1} character long.")]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
