using System;
using System.Collections.Generic;
using System.Text;

namespace Consul4NetCore
{
    public class ConsulConfig
    {
        public static ConsulConfig Instance { get; set; }

        /// <summary>
        /// 是否在Consul上注册服务
        /// </summary>
        public bool IsRegisterService { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string ServiceAddress { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        public int ServicePort { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// consul注册中心ip
        /// </summary>
        public string ConsulIP { get; set; }
        /// <summary>
        /// consul注册中心port
        /// </summary>
        public int ConsulPort { get; set; }
        /// <summary>
        /// 健康检查接口，默认api/Health
        /// </summary>
        public string HealthCheckApi { get; set; }
        /// <summary>
        /// 健康检查间隔时间(秒)
        /// </summary>
        public int HealthCheckInterval { get; set; }
        /// <summary>
        /// 同步可用的服务名称集
        /// </summary>
        public string[] SyncServiceNames { get; set; }
        /// <summary>
        /// 同步可用服务信息间隔时间(秒)
        /// </summary>
        public int SyncServiceInterval { get; set; }

    }
}
