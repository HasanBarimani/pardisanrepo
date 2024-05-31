using Newtonsoft.Json;
using Pardisan.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Pardisan.Services
{
    public class HelperService : IHelperService
    {

        public async Task<RootApi> CallApi(string apiUrl, string method, string authorization = "", string jsonData = "")
        {
            var result = await SendRequest(apiUrl, method, authorization, jsonData);
            return JsonConvert.DeserializeObject<RootApi>(result);
        }

        public async Task<RootApi<TResponse>> CallApi<TResponse>(string apiUrl, string method, string authorization = "", string jsonData = "")
        {
            var result = await SendRequest(apiUrl, method, authorization, jsonData);
            var mamad = JsonConvert.DeserializeObject<RootApi<TResponse>>(result);
            return mamad;
        }


        private async Task<string> SendRequest(string apiUrl, string method, string authorization = "", string jsonData = "")
        {

            string result = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                HttpMethod httpMethod = new HttpMethod(method);

                HttpRequestMessage request = new HttpRequestMessage(httpMethod, apiUrl);

                if (!string.IsNullOrEmpty(authorization))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                }

                if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                {
                    request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await httpClient.SendAsync(request);

                result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Invalid authorization token.");
                }
            }

            return result;
        }



        public class RootApi
        {
            public object message { get; set; }
            public List<string> errors { get; set; }
            public int statusCode { get; set; }
            public dynamic data { get; set; }
        }

        public class RootApi<TData>
        {
            public string message { get; set; }
            public List<string> errors { get; set; }
            public int statusCode { get; set; }
            public TData data { get; set; }
        }
    }
}
