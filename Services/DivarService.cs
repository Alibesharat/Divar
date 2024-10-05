using System.Text;
using System.Web;
using divar.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace divar.Services
{
    public class DivarService
    {
        private readonly HttpClient _httpClient;
        private readonly DivarSetting _divarSetting;

        public DivarService(HttpClient httpClient, IOptions<DivarSetting> divarSetting)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _divarSetting = divarSetting.Value;
        }

        /// <summary>
        /// دریافت اطلاعات پست در دیوار
        /// </summary>
        /// <param name="PostToken"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>

        public async Task<PostData> GetPostDataAsync(string PostToken, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(PostToken))
                throw new ArgumentException("Token must not be null or empty", nameof(PostToken));
            var GetPostDataUrl = $"https://api.divar.ir/v1/open-platform/finder/post/{PostToken}";

            // Set up the request headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _divarSetting.ApiToken);
            _httpClient.DefaultRequestHeaders.Add("x-access-token", accessToken);

            try
            {
                // Send GET request
                var response = await _httpClient.GetAsync(GetPostDataUrl);
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not success

                // Read response content
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize JSON to object
                var postData = JsonConvert.DeserializeObject<PostData>(jsonResponse);

                return postData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        public string GenerateAuthorizationUrl(string stateData)
        {
            //BUG BUG : Double check the whole workFlow , it is not clear from third party !

               // Generate salt (random string)
        var salt = GenerateSalt(10);

        string state;
        if (!string.IsNullOrEmpty(stateData))
        {
            // Combine stateData with salt and encrypt
            string dataToEncrypt = $"{stateData}\n{salt}";
            string encryptedState = EncryptionHelper.Encrypt(dataToEncrypt);

            // Base64 URL encode
            state = Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptedState));
        }
        else
        {
            // If no stateData, just use salt
            state = salt;
        }

        // Create the scopes (adjust as needed)
        string scopes = HttpUtility.UrlEncode("USER_PHONE CHAT_MESSAGE_SEND");

        // Build the OAuth URL
        var authorizationUrl = $"https://api.divar.ir/oauth2/auth?response_type=code" +
                               $"&client_id={_divarSetting.ClientId}" +
                               $"&redirect_uri={HttpUtility.UrlEncode(_divarSetting.RedirectUrl)}" +
                               $"&scope={scopes}" +
                               $"&state={state}";

        return authorizationUrl;
          
        }

        public async Task<string> ExchangeCodeForAccessTokenAsync(string code, string redirectUri)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code must not be null or empty", nameof(code));
            if (string.IsNullOrWhiteSpace(redirectUri))
                throw new ArgumentException(
                    "RedirectUri must not be null or empty",
                    nameof(redirectUri)
                );

            var tokenRequestBody = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _divarSetting.ClientId },
                { "client_secret", _divarSetting.ClientSecret },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUri },
            };

            try
            {
                var content = new FormUrlEncodedContent(tokenRequestBody);
                var response = await _httpClient.PostAsync(
                    "https://api.divar.ir/oauth2/token",
                    content
                );
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic tokenResponse = JsonConvert.DeserializeObject(jsonResponse);
                return tokenResponse.access_token;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Token request error: {e.Message}");
                return null;
            }
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException(
                    "RefreshToken must not be null or empty",
                    nameof(refreshToken)
                );

            var refreshRequestBody = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", _divarSetting.ClientId },
                { "client_secret", _divarSetting.ClientSecret },
                { "refresh_token", refreshToken },
                { "redirect_uri", _divarSetting.RedirectUrl },
            };

            try
            {
                var content = new FormUrlEncodedContent(refreshRequestBody);
                var response = await _httpClient.PostAsync(
                    "https://api.divar.ir/oauth2/token",
                    content
                );
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic tokenResponse = JsonConvert.DeserializeObject(jsonResponse);
                return tokenResponse.access_token;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Refresh token error: {e.Message}");
                return null;
            }
        }

        private static string GenerateSalt(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var salt = new char[length];

            for (int i = 0; i < length; i++)
            {
                salt[i] = chars[random.Next(chars.Length)];
            }

            return new string(salt);
        }
    }
}
