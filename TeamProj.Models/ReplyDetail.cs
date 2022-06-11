using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class ReplyDetail
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
