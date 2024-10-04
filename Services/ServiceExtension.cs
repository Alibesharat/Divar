using System.Security.Cryptography.X509Certificates;
using divar.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtension
    {
        public static void AddDivarService(this IServiceCollection services)
        {
            services.AddHttpClient<DivarService>(client =>
              {
                  // Other client configurations if needed
              });
      }

        public static void AddSmsService(this IServiceCollection services)
        {
           
            services.AddSingleton<SmsService>();

        }
    }
}