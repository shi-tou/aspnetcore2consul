/*********************************************************************
*Copyright (c) 2018 深圳房讯通信息技术有限公司 All Rights Reserved.
*CLR版本： .NET Core SDK 2.0
*公司名称：深圳房讯通信息技术有限公司
*命名空间：Fxt.Framework.Caching.Redis
*文件名：  RedisExtensions
*版本号：  V1.0.0.0
*创建人：  Mibin
*创建时间：2018-7-6 14:33:07
*描述：
*
*--------------多次修改可添加多块注释---------------
*修改时间：2018-7-6 14:33:07
*修改人： Mibin
*描述：first create
*
**********************************************************************/
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
