using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consul4NetCore
{
    public class ConsulCache
    {
        private ConsulCache() { }

        private static ConsulCache _instance { get; set; }

        /// <summary>
        /// 单例实例 
        /// </summary>
        public static ConsulCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(ConsulCache))
                    {
                        if (_instance == null)
                        {
                            _instance = new ConsulCache();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 可用服务信息
        /// </summary>
        private Dictionary<string, ServiceEntry[]> _serviceInfos = new Dictionary<string, ServiceEntry[]>();

        /// <summary>
        /// 设置服务信息，无则添加，有则更新
        /// </summary>
        /// <param name="name">服务名，无法自动识别服务名时再参考</param>
        /// <param name="services">服务信息</param>
        /// <returns>成功设置数量</returns>
        public bool SetServiceInfo(string name, params ServiceEntry[] services)
        {
            if (_serviceInfos.ContainsKey(name))
            {
                _serviceInfos.Remove(name);
            }
            _serviceInfos.Add(name, services);
            return true;
        }

        /// <summary>
        /// 获取服务连接信息，格式为{ip}:{port}
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns>可用Host字符串数组</returns>
        public string[] GetServiceHosts(string serviceName)
        {
            if (!_serviceInfos.ContainsKey(serviceName))
            {
                return new string[0];
            }
            var services = _serviceInfos[serviceName];
            return services.Select(service => service.Node.Address + ":" + service.Service.Port).ToArray();
        }

        /// <summary>
        /// 获取服务地址
        /// </summary>
        /// <param name="client"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public Uri LookupService(string serviceName)
        {
            var services = GetServiceHosts(serviceName);
            int count = services.Count();
            if (count == 0)
            {
                throw new Exception($"not exist the service instance for servicename '{serviceName}'");
            }
            string host = services[new Random().Next(count)];
            if (!host.StartsWith("http://"))
                host = "http://" + host;
            return new Uri(host);
        }
    }
}
