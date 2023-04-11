using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace EPS.API.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireReferrerAttribute : ActionMethodSelectorAttribute
    {
        public RequireReferrerAttribute(params string[] trustedServers)
        {
            TrustedServers = trustedServers;
        }

        public string[] TrustedServers { get; }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            string referrer = routeContext.HttpContext.Request.Headers["Referer"];

            if (string.IsNullOrEmpty(referrer))
                return false;

            var list = new List<string>(TrustedServers);
            var uri = referrer.ToLower();
            return (list.Contains(referrer));
        }
    }    
}
