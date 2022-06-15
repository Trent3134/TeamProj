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
            CreateMap<ReplyEntity, ReplyListItem>().ReverseMap();

            CreateMap<ReplyModel, ReplyEntity>().ReverseMap();

            CreateMap<ReplyUpdate, ReplyEntity>().ReverseMap();
        }
    }
