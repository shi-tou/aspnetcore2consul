using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore2consul
{
    public class ConsulConfig
    {
        /// <summary>
        /// 应用服务ip
        /// </summary>
        public string ServiceIP { get; set; }
        /// <summary>
        /// 应用服务端口
        /// </summary>
        public int ServicePort { get; set; }
        /// <summary>
        /// 应用服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// consul注册中心ip
        /// </summary>
        public string ConsulIP { get; set; }
        /// <summary>
        /// consul注册中心ip
        /// </summary>
        public int ConsulPort { get; set; }
    }
}
