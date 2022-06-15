using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class ReplyModel
    {
        public string Reply { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
