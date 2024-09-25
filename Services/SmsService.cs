using System.Text;
using mpNuget;
using Newtonsoft.Json;

namespace divar.Services
{
    public class SmsService
    {
        const string username = "09190078747";
        const string password = "7L8!Y";
        

        private readonly HttpClient client;
        private readonly string ApiToken;

        int _CustomerBodyId;
        int _ExpertBodyId;
        public SmsService(HttpClient HttpClient)
        {
            client = HttpClient;
            client.BaseAddress = new Uri("https://console.melipayamak.com");
            ApiToken = "9bf4fd07641740b3a467da5b34f2cac3";
            _CustomerBodyId = 252427;
            _ExpertBodyId = 252428;
            
        }

        public void SendMessageToCustomer(string customerName,string expertName,string to)
        {
            string[] args =[customerName,expertName];
            client.PostAsJsonAsync($"api/send/shared/{ApiToken}", new { _CustomerBodyId, to, args });
               
        }
        public void SendMessageExpert(string customerName,string expertName,string to)
        {
             string[] args =[expertName,customerName];
             client.PostAsJsonAsync($"api/send/shared/{ApiToken}", new { _ExpertBodyId, to, args });
               
        }
        
    }
}