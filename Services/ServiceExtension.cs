using divar.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtension
    {
        public static void AddDivarService(this IServiceCollection services, string apiKey)
        {
            services.AddHttpClient<DivarService>(client =>
              {
                  // Other client configurations if needed
              });
            services.AddTransient<DivarService>(provider =>
      {
          var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
          return new DivarService(httpClient, apiKey);
      });

        }
    }
}