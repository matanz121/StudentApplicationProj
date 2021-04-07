using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StudentsApplicationProj.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApplicationProj.Client.Services
{
    public interface IClientAuthService
    {
        UserToken User { get; }
        Task Initialize();
        Task Login(LoginRequest loginModel);
        Task Register(RegisterRequest registerModel);
        Task Logout();
    }

    public class ClientAuthService : IClientAuthService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;


        public ClientAuthService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorage
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
        }

        public UserToken User { get; private set; }

        public async Task Initialize()
        {
            User = await _localStorage.GetItemAsync<UserToken>("user");
        }

        public async Task Login(LoginRequest loginModel)
        {
            User = await _httpService.Post<UserToken>("/api/auth/login", loginModel);
            await _localStorage.SetItemAsync("user", User);
        }

        public async Task Register(RegisterRequest registerModel)
        {
            await _httpService.Post<object>("/api/auth/register", registerModel);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorage.RemoveItemAsync("user");
            _navigationManager.NavigateTo("/auth/login");
        }
    }
}
