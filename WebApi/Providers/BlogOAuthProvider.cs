using BLL.DTO;
using BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.DependencyInjection;

namespace WebApi.Providers
{
    /// <summary>
    /// Provides a authorization by tokens.
    /// </summary>
    public class BlogOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAccountService _accountService = BlogDependencyInjection.GetKernel().Get<IAccountService>();

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserDTO user = await Task.Run(() => _accountService.FindUser(context.UserName, context.Password));

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = _accountService.CreateIdentity(user.Id, OAuthDefaults.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}