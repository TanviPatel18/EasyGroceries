using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace ECommerceUI.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public CustomAuthStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        private ClaimsPrincipal _anonymous =
            new ClaimsPrincipal(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var role = await _js.InvokeAsync<string>("localStorage.getItem", "role");

            if (string.IsNullOrEmpty(role))
                return new AuthenticationState(_anonymous);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, role)
            }, "apiauth");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string role)
        {
            var identity = new ClaimsIdentity(new[]
            {
        new Claim(ClaimTypes.Role, role)
    }, "apiauth");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}