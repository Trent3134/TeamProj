using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class ReplyUpdate
    {
        public int Id { get; set; }
        [MinLength(1, ErrorMessage = "{0} mist be at least {1} characters long.")]
        [MaxLength(500, ErrorMessage = "{0} mist contain no more than {1} characters")]
        public string Reply { get; set; }
    }
