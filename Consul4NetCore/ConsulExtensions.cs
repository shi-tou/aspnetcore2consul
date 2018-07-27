
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consul4NetCore
{
    public static class ConsulExtensions
    {
        /// <summary>
        /// consul配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <param name="optional"></param>
        /// <param name="reloadOnChange"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsulFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            builder.AddJsonFile(path, optional, reloadOnChange);
            IConfigurationRoot configuration = builder.Build();
            ConsulConfig.Instance = configuration.GetSection("ConsulConfig").Get<ConsulConfig>();
            return builder;
        }

        /// <summary>
        /// 注册consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            services.AddSingleton<ConsulService>();
            return services;
        }

        /// <summary>
        /// 注册consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        { 
            var consulService = app.ApplicationServices.GetService<ConsulService>();
            consulService.Start();
            return app;
        }
    }
}
