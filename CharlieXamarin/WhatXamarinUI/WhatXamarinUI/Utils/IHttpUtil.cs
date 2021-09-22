using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhatXamarinUI.Models;

namespace WhatXamarinUI.HttpUtils
{
    public interface IHttpUtil
    {
        Task<bool> Authorize(AuthModel authModel, string url);

        void Logout();

        Task<HttpResponseMessage> GetAsync(string url);

        Task<HttpResponseMessage> PostJsonAsync<T>(string url, T postData);

        Task<HttpResponseMessage> PutJsonAsync<T>(string url, T postData);

        Task<HttpResponseMessage> DeleteAsync(string url);

        Task<HttpResponseMessage> PatchAsync(string url);

        bool IsSuccessfulStatusCode(HttpResponseMessage httpResponse);
    }
}
