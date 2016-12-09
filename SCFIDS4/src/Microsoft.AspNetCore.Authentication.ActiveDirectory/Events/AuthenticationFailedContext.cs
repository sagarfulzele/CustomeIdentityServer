﻿namespace Microsoft.AspNetCore.Authentication.ActiveDirectory.Events
{
    using Microsoft.AspNetCore.Http;

    public class AuthenticationFailedContext : BaseActiveDirectoryContext
    {
        public AuthenticationFailedContext(HttpContext context, ActiveDirectoryOptions options)
               : base(context, options)
        {
        }
    }
}
