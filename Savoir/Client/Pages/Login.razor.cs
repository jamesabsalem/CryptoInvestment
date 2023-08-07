using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Savoir.Client.Resources;
using Savoir.Client.Services;

namespace Savoir.Client.Pages
{
    public partial class Login
    {
        private LoginModel Model = new();
        //[Inject] private IAuthService AuthService { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var user = await AuthService.GetUser();
            if (user != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        private async void OnValidSubmit()
        {
            try
            {
                var user = await AuthService.Login(Model);
                if (user != null)
                {
                    NavigationManager.NavigateTo("/");
                    ToastService.ShowInfo("Welcome to Savoir.", user.UserName);
                } else
                {
                    ToastService.ShowError("Invalid user & password.", "Invalid");
                }
            }
            catch (Exception e)
            {
                ToastService.ShowError(e.Message);
            }
        }
    }
}
