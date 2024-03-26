using GodotNet_LegendOfPaladin.SceneModels;
using GodotNet_LegendOfPaladin.Utils;
using GodotProgram.Services;
using GodotProgram.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotNet_LegendOfPaladin
{
    public class Program
    {
        /// <summary>
        /// IOC容器
        /// </summary>
        public static IServiceProvider Services = ConfigureServices();
        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var builder = new ServiceCollection();
            AddServices(builder);
            AddSceneModel(builder);
            return builder.BuildServiceProvider();
        }
        /// <summary>
        /// 添加服务，应以Singleton形式添加
        /// </summary>
        /// <param name="service"></param>
        public static void AddServices(ServiceCollection builder)
        {
            builder.AddSingleton<TestService>();
            builder.AddSingleton<NlogHelper>();
            builder.AddSingleton<FreeSqlHelper>();
            builder.AddSingleton<PackedSceneHelper>();
        }
        /// <summary>
        /// 添加SceneModel，应以Transient添加
        /// </summary>
        /// <param name="service"></param>
        public static void AddSceneModel(ServiceCollection builder)
        {
            builder.AddTransient<MainSceneModel>();
        }
    }
}
