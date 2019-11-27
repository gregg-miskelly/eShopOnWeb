using System;
using System.Runtime.Serialization;

namespace Microsoft.eShopWeb.Web.Services
{
    internal class ServiceAuthenticationException : Exception
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}