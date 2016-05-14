using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Securing.AspNet.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAjaxAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public const string AntiForgeryTokenFieldName = "__RequestVerificationToken";
        public const string AntiForgeryTokenHeaderName = "X-CSRF-Token";

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var request = filterContext.HttpContext.Request;
            if (request.IsAjaxRequest() && !request.Form.AllKeys.Contains(AntiForgeryTokenFieldName))
            {
                // Validate using the value from a custom header as the form token
                if (request.Headers.AllKeys.Contains(AntiForgeryTokenHeaderName))
                {
                    var headerToken = request.Headers[AntiForgeryTokenHeaderName];
                    var cookieToken = GetCookieCsrfTokenValue(request);
                    AntiForgery.Validate(cookieToken, headerToken);

                    return;
                }
            }

            // Use the default validation as fallback
            AntiForgery.Validate();
        }

        private string GetCookieCsrfTokenValue(HttpRequestBase request)
        {
            if (request.Cookies.AllKeys.Contains(AntiForgeryConfig.CookieName))
            {
                return request.Cookies[AntiForgeryConfig.CookieName].Value;
            }

            return null;
        }
    }
}