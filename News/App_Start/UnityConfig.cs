using AutoMapper;
using News.Controllers;
using News.DTO;
using News.Interface;
using News.Models;
using News.Repositories;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace News
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICategoriesRepository, CategoriesRepository>();
            container.RegisterType<IPostsRepository, PostsRepository>();
            container.RegisterType<ICommentsRepository, CommentsRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IRolesRepository, RolesRepository>();
            container.RegisterType<IChartsRepository, ChartsRepository>();
            container.RegisterType<IAppSettingsRepository, AppSettingsRepository>();
            container.RegisterType<IModulesRepository, ModulesRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<RolesController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, Users>();
                cfg.CreateMap<Users, UserViewModel>();
                cfg.CreateMap<CommentViewModel, Comments>();
                cfg.CreateMap<Comments, CommentViewModel>();
                cfg.CreateMap<CategoryViewModel, Categories>();
                cfg.CreateMap<Categories, CategoryViewModel>();
                cfg.CreateMap<PostViewModel, Posts>();
                cfg.CreateMap<Posts, PostViewModel>();
                cfg.CreateMap<PostList, PostViewModel>();
                cfg.CreateMap<AppSettings, AppSettingViewModel>();
                cfg.CreateMap<Modules, ModuleViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);
        }
    }
}