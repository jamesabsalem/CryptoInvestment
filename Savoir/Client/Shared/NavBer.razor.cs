using Microsoft.AspNetCore.Components;
using Savoir.Client.Services;

namespace Savoir.Client.Shared
{
    public partial class NavBer
    {
        [Inject]
        private IAuthService _authService { get; set; }
        private async void OnClickLogout()
        {
            await _authService.Logout();
        }
    }
}
