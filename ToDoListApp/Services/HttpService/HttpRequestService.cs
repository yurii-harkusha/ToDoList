using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Services.HttpService
{
    public class HttpRequestService
    {
        private string BaseURL = "";
        private static readonly HttpClient _httpClient = new HttpClient();

        public HttpRequestService()
        {
        }

        public async Task<T> Get<T>(string command)
        {
            return await SendRequest<T>(command, HttpMethod.Get);
        }

        public async Task<T> Post<T>(string command, object data)
        {
            return await SendRequest<T>(command, HttpMethod.Post, data);
        }

        private async Task<T> SendRequest<T>(string command, HttpMethod method, object data = null)
        {
            string url = BaseURL + command;
            var dataJson = String.Empty;
            StringContent content = new StringContent("");
            if (data != null)
            {
                dataJson = JsonConvert.SerializeObject(data);
                content = new StringContent(dataJson.ToString(), Encoding.UTF8, "application/json");
            }
            try
            {
                using (var request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri(url);
                    request.Method = method;

                    if(content != new StringContent(""))
                    {
                        request.Content = content;
                    }

                    var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await DeserializeResponse<T>(response);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(T);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            try
            {
                if (response.Content != null)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    {
                        string text = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(text);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(T);
        }
    }
}
