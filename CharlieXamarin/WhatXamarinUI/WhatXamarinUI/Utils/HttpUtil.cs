using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WhatXamarinUI.Models;

namespace WhatXamarinUI.HttpUtils
{
    public class HttpUtil : IHttpUtil
    {
        private readonly HttpClient _client;

        private static HttpUtil _instance;

        private string BearerToken { get; set; }

        private HttpUtil()
        {
            _client = new HttpClient();
        }

        public static HttpUtil GetInstance()
        {
            if(_instance == null)
            {
                _instance = new HttpUtil();
            }
            return _instance;
        }

        public async Task<bool> Authorize(AuthModel authModel, string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://whatbackendapihosting.azurewebsites.net/api/accounts/auth");

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(authModel), Encoding.UTF8, "application/json");

            var responseMessage = await _client.SendAsync(requestMessage);

            responseMessage.Headers.TryGetValues("Authorization", out IEnumerable<string> token);

            if (token == null)
            {
                return false;
            }

            BearerToken = token.FirstOrDefault().Replace("Bearer ", "");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

            return true;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);

            var responseMessage = await _client.SendAsync(requestMessage);

            return responseMessage;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage httpResponse = await _client.SendAsync(requestMessage);

            return httpResponse;
        }

        public async Task<HttpResponseMessage> PatchAsync(string url)
        {
            var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), url);

            var responseMessage = await _client.SendAsync(requestMessage);

            return responseMessage;
        }

        public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T postData)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var responseMessage = await _client.SendAsync(requestMessage);

            return responseMessage;
        }

        public async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T postData)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var responseMessage = await _client.SendAsync(requestMessage);

            return responseMessage;
        }

        public bool IsSuccessfulStatusCode(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public void Logout()
        {
            BearerToken = "";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        }
    }
}
