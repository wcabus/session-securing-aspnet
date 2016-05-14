using System;
using System.Linq;
using System.Web.Http.Filters;
using System.Web.Helpers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Securing.AspNet.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateApiAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public const string AntiForgeryTokenHeaderName = "X-CSRF-Token";

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var request = actionContext.Request;

            // Validate using the value from a custom header as the form token
            if (request.Headers.Contains(AntiForgeryTokenHeaderName))
            {
                var headerValue = request.Headers.GetValues(AntiForgeryTokenHeaderName).First().Split(':');
                var cookieToken = headerValue[0];
                var formToken = headerValue[1];
                AntiForgery.Validate(cookieToken, formToken);

                return continuation();
            }

            // Use the default validation as fallback
            AntiForgery.Validate();

            return continuation();
        }
    }
}