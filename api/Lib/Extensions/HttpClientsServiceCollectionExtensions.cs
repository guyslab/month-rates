using Lib.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Lib.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddApiLayerDotComClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton(sp => {
                var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
                var options = sp.GetRequiredService<IOptions<RateServiceOptions>>();
                client.BaseAddress = new Uri(options.Value.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("apikey", options.Value.ApiKey);

                return client;
            });

            return services;
        }
    }
}
