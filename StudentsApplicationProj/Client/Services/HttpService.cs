using Blazored.LocalStorage;
using StudentsApplicationProj.Client.Models;
using StudentsApplicationProj.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Client.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task<T> Put<T>(string uri, object value);
        Task<T> Delete<T>(string uri);
        Task<T> Post<T>(string uri, object value);
        Task<object> UploadFiles(MultipartFormDataContent content, string uri);
    }

    public class HttpService: IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public HttpService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }

        public async Task<T> Delete<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await sendRequest<T>(request);
        }

        public async Task<object> UploadFiles(MultipartFormDataContent content, string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = content
            };
            return await sendRequest<object>(request);
        }

        private async Task<T> sendRequest<T>(HttpRequestMessage request)
        {
            var user = await _localStorage.GetItemAsync<UserToken>("user");
            if(user != null)
            {
                request.Headers.Add("Authorization", "Bearer " + user.Token);
            }
            using var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
