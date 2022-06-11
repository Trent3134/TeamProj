using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class ReplyListItem
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
