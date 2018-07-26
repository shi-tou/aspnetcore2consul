using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Consul4NetCore
{
    public class ConsulHttpClient
    {
        /// <summary>
        ///  HttpClient
        /// </summary>
        private HttpClient _httpClient { get; set; }
        /// <summary>
        /// 请求头信息
        /// </summary>
        private WebHeaderCollection _headers { get; set; }
        /// <summary>
        /// Content-Type,默认application/json
        /// </summary>
        private string _contentType { get; set; }
        /// <summary>
        /// 编码,默认utf-8
        /// </summary>
        private Encoding _encodingType { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        private string _serviceName { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        public ConsulHttpClient()
        {
            _headers = new WebHeaderCollection();
            _contentType = "application/json";
            _encodingType = Encoding.UTF8;
        }

        /// <summary>
        /// set headers
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetHeader(string name, string value)
        {
            _headers.Add(name, value);
        }
        /// <summary>
        /// set contentType
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetContentType(string contentType)
        {
            _contentType = contentType;
        }

        /// <summary>
        /// set contentType
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetEncodingType(Encoding encodingType)
        {
            _encodingType = encodingType;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoGet(string serviceName,string requestUrl)
        {
            return DoRequest(serviceName,requestUrl, HttpMethod.Get);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoPost(string serviceName, string requestUrl, string requestData="")
        {
            return DoRequest(serviceName, requestUrl, HttpMethod.Post, requestData);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoPut(string serviceName, string requestUrl, string requestData = "")
        {
            return DoRequest(serviceName, requestUrl, HttpMethod.Put, requestData);
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string DoDelete(string serviceName, string requestUrl)
        {
            return DoRequest(serviceName, requestUrl, HttpMethod.Delete);
        }

        /// <summary>
        /// 请求url
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private string DoRequest(string serviceName,string requestUrl, HttpMethod httpMethod, string requestData = "")
        {
            try
            {
                Uri url = ConsulCache.Instance.LookupService(serviceName);
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = url;
                //请求内容
                HttpContent httpContent = new StringContent(requestData);
                //Headers
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                foreach (string key in _headers.AllKeys)
                {
                    httpContent.Headers.Add(key, _headers[key]);
                }
                HttpResponseMessage httpResponse = null;
                //Get
                if (httpMethod == HttpMethod.Get)
                {
                    httpResponse = _httpClient.GetAsync(requestUrl).Result;
                }
                //Post
                else if (httpMethod == HttpMethod.Post)
                {
                    httpResponse = _httpClient.PostAsync(requestUrl, httpContent).Result;
                }
                //Put
                else if (httpMethod == HttpMethod.Put)
                {
                    httpResponse = _httpClient.PutAsync(requestUrl, httpContent).Result;
                }
                //Put
                else if (httpMethod == HttpMethod.Delete)
                {
                    httpResponse = _httpClient.DeleteAsync(requestUrl).Result;
                }
                //获取结果
                return httpResponse.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("ConsulHttpClient.DoRequest", ex.Message);
                return "";
            }
            finally
            {
                _httpClient.Dispose();
            }
        }
    }
}
