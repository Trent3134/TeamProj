public class PostMapProfile : Profile
{
    public PostMapProfile()
    {
        CreateMap<PostEntity, PostDetail>();
        CreateMap<PostEntity, PostListItem>();

        CreateMap<PostCreate, PostEntity>()
            .ForMember(post => post.CreatedUtc,
                opt => opt.MapFrom(src => DateTimeOffset.Now)
                );

        CreateMap<PostUpdate, PostEntity>()
            .ForMember(post => post.CreatedUtc,
                opt => opt.MapFrom(src => DateTimeOffset.Now)
                );
    }
}