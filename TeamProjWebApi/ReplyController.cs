using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : Controller
    {   
        private readonly IReplyService _rservice;
        public ReplyController(IReplyService rservice)
        {
            _rservice = rservice;
        }
    }