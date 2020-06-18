using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infraestructura.Autenticacion
{
    public sealed class BlazorAuthenticationProvider: AuthenticationStateProvider
    {
        private readonly HttpClient _http;

        public BlazorAuthenticationProvider(HttpClient http)
        {
            _http = http;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
