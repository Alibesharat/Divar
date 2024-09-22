using mpNuget;

namespace divar.Services
{
    public class SmsService
    {
        const string username = "09190078747";
        const string password = "7L8!Y";
        const string from = "5000...";
        const bool isFlash = false;

        private readonly RestClient restClient;
        public SmsService()
        {
            restClient = new RestClient(username,password);
        }

        public void SendMessage(string To,string Message)
        {
          var result =  restClient.Send(To, from, Message, isFlash);
          System.Console.WriteLine("Sms result : ", result.Value);
        }
    }
}