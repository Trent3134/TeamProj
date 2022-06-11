using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;



    public class ReplyMap : Profile
    {
        public ReplyMap()
        {
            CreateMap<ReplyEntity, ReplyDetail>();
            CreateMap<ReplyEntity, ReplyListItem>();

            CreateMap<ReplyCreate, ReplyEntity>()
            .ForMember(reply => reply.CreatedUtc,
            OperatingSystem => opt.MapFrom(SearchOption => DateTimeOffset.Now));

            CreateMap<ReplyUpdate, ReplyEntity>()
            .ForMember(reply => reply.ModifiedUtc,
            OperatingSystem => opt.MapFrom(SearchOption => DateTimeOffset.Now));
        }
    }
