using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TeamProj.Models.Comments;

public class Mapers : Profile 
    {
        public Mapers()
        {
            CreateMap<CommentsEntity, CommentDetail>();
            CreateMap<CommentsEntity, CommentListItem>();

            CreateMap<CommentCreate, CommentsEntity>()
            .ForMember(comment => comment.CreatedAt,
            opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<CommentsUpdate, CommentsEntity>()
            .ForMember(comment => comment.ModifiedAt,
            opt => opt.MapFrom(src => DateTimeOffset.Now));

        }
    }
