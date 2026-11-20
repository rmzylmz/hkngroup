namespace HKNGroup.Application.Common.Mappings
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<SiteSetting, SiteSettingsDto>().ReverseMap();
            CreateMap<SiteSetting, SiteSettingsUpdateDto>().ReverseMap();
            CreateMap<SiteSetting, SiteSettingsListDto>().ReverseMap();
            
            //

            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<Page, PageAddDto>().ReverseMap();
            CreateMap<Page, PageUpdateDto>().ReverseMap();
            CreateMap<Page, PageListDto>().ReverseMap();
            // 

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Message, MessageAddDto>().ReverseMap();
            CreateMap<Message, MessageUpdateDto>().ReverseMap();
            CreateMap<Message, MessageListDto>().ReverseMap();
            CreateMap<Message, MessageListDto>().ReverseMap();
             
            //

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectAddDto>().ReverseMap();
            CreateMap<Project, ProjectUpdateDto>().ReverseMap();
            CreateMap<Project, ProjectListDto>().ReverseMap();
            CreateMap<ProjectImages, ProjectImagesAddDto > ().ReverseMap();
            // 

            CreateMap<ProjectCategory, ProjectCategoryDto>().ReverseMap();
            CreateMap<ProjectCategory, ProjectCategoryAddDto>().ReverseMap();
            CreateMap<ProjectCategory, ProjectCategoryUpdateDto>().ReverseMap();
            CreateMap<ProjectCategory, ProjectCategoryListDto>().ReverseMap();
            //

            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Slider, SliderAddDto>().ReverseMap();
            CreateMap<Slider, SliderUpdateDto>().ReverseMap();
            CreateMap<Slider, SliderListDto>().ReverseMap();
            // 
            CreateMap<OurService, OurServicesDto>().ReverseMap();
            CreateMap<OurService, OurServicesAddDto>().ReverseMap();
            CreateMap<OurService, OurServicesUpdateDto>().ReverseMap();
            CreateMap<OurService, OurServicesListDto>().ReverseMap();
            //

            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<Article, ArticleAddDto>().ReverseMap();
            CreateMap<Article, ArticleUpdateDto>().ReverseMap();
            CreateMap<Article, ArticleListDto>().ReverseMap();
            // 

            CreateMap<ArticleComment, ArticleCommentDto>().ReverseMap();
            CreateMap<ArticleComment, ArticleCommentAddDto>().ReverseMap();
            CreateMap<ArticleComment, ArticleCommentUpdateDto>().ReverseMap();
            CreateMap<ArticleComment, ArticleCommentListDto>().ReverseMap();
            // 

            CreateMap<ArticleCategory, ArticleCategoryDto>().ReverseMap();
            CreateMap<ArticleCategory, ArticleCategoryAddDto>().ReverseMap();
            CreateMap<ArticleCategory, ArticleCategoryUpdateDto>().ReverseMap();
            CreateMap<ArticleCategory, ArticleCategoryListDto>().ReverseMap();
            // 

            CreateMap<Admin, AdminDto>().ReverseMap();
            CreateMap<Admin, AdminLoginDto>().ReverseMap();
            CreateMap<Admin, AdminUpdateDto>().ReverseMap();


            //
            CreateMap<Menus, MenusDto>().ReverseMap();
            CreateMap<Menus, MenusAddDto>().ReverseMap();
            CreateMap<Menus, MenusUpdateDto>().ReverseMap();
            CreateMap<Menus, MenusListDto>().ReverseMap();

            //
            CreateMap<PopupMessages, PopupMessagesDto>().ReverseMap();
            CreateMap<PopupMessages, PopupMessagesAddDto>().ReverseMap();
            CreateMap<PopupMessages, PopupMessagesUpdateDto>().ReverseMap();
            CreateMap<PopupMessages, PopupMessagesListDto>().ReverseMap();

            //
            CreateMap<Referance, ReferanceDto>().ReverseMap();
            CreateMap<Referance, ReferanceAddDto>().ReverseMap();
            CreateMap<Referance, ReferanceUpdateDto>().ReverseMap();
            CreateMap<Referance, ReferanceListDto>().ReverseMap();
        }
    }
}


