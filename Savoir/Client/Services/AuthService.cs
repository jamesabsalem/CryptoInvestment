using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using System.Net.Http.Json;

namespace Savoir.Client.Services
{
    public class AuthService : IAuthService
    {
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";
        private readonly HttpClient _httpClient;
        private const string ControllerName = "api/Auth";

        public User User { get; private set; }

        public AuthService(
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            HttpClient httpClient
        )
        {
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task Initialize()
        {
            this.User = await _localStorageService.GetItem<User>(_userKey);
            if(User == null)
            {
                _navigationManager.NavigateTo("/Login");
            }
        }
        public async Task<User> GetUser()
        {
            return await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task<User> Login(LoginModel model)
        {
            using var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/login", model);
            if (response.IsSuccessStatusCode)
            {
                User = await response.Content.ReadFromJsonAsync<User>();
                await _localStorageService.SetItem(_userKey, User);
                return User;
            }
            return null;
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("/Login");
        }
    }
}