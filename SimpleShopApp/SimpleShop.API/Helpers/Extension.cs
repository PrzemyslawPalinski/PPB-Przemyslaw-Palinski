using System;
using Microsoft.AspNetCore.Http;

namespace SimpleShop.API.Helpers
{
    public static class Extension
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Aplication-Error");
            response.Headers.Add("Access-Control-Require-Origin", "*");
        }
    }
}