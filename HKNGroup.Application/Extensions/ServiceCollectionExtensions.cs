using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HKNGroup.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IReferanceService, ReferanceManager>();
            services.AddScoped<IPopupMessagesService, PopupMessagesManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<ISiteSettingsService, SiteSettingsManager>();
            services.AddScoped<IPageService, PageManager>();
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryManager>();
            services.AddScoped<IArticleCommentService, ArticleCommentManager>();
            services.AddScoped<IMenusService, MenusManager>();
            services.AddScoped<IOurServicesService, OurServiceManager>();
            services.AddScoped<IProjectImagesService, ProjectImagesManager>();
            services.AddScoped<IProjectService, ProjectManager>();
            services.AddScoped<IProjectCategoryService, ProjectCategoryManager>();
            return services;
        }

        public static IServiceCollection LoadMyService(this IServiceCollection services, IConfiguration config)
        {
            return services.AddApplicationServices();
        }
    }
}

