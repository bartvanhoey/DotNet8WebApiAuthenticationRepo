
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text;
using System.Text.Json;
using System.Net;
using DotNet8BlazorWebApp.Authentication;
using DotNet8BlazorWebApp.Extensions;

namespace T3App.Blazor.Authentication
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(IHttpClientFactory clientFactory, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = clientFactory.CreateClient("ServerAPI");

            // if (_httpClient.BaseAddress == null)
            // {
            //     _httpClient.BaseAddress = new Uri("https://localhost:44370");
            // }
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync<RegisterModel>("api/accounts", registerModel);

            return new RegisterResult(); ;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/authenticate/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var jsonContent = await response.Content.ReadAsStringAsync();
            var loginResult = jsonContent.ConvertJsonTo<LoginResult>(); 

            if (!response.IsSuccessStatusCode)
            {
                loginResult.Successful = false;
                return loginResult;
            }

            Console.WriteLine(loginResult.AccessToken);

            await _localStorage.SetItemAsync("authToken", loginResult.AccessToken);
            // ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            // ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            var response = await _httpClient.PostAsync("api/account/logout", null);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            if (response.StatusCode != HttpStatusCode.OK) throw new Exception("something went wrong while logging out");
        }
    }
}