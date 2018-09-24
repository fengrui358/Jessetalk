using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ThirdPartClientSample
{
    class Program
    {
        static async Task Main()
        {
            var discoveryResponse = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (discoveryResponse.IsError)
            {
                Console.WriteLine(discoveryResponse.Error);
            }

            PrintResult(
                await GetResourceWithToken(await GetTokenWithClientCredentials(discoveryResponse.TokenEndpoint)),
                "ClientCredentials");

            PrintResult(
                await GetResourceWithToken(await GetTokenWithResourceOwnerPassword(discoveryResponse.TokenEndpoint)),
                "ResourceOwnerPassword");

            Console.ReadKey();
        }

        private static async Task<string> GetTokenWithClientCredentials(string tokenEndpoint)
        {
            var tokenClient = new TokenClient(tokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync();

            Console.WriteLine(tokenResponse.IsError ? tokenResponse.ErrorDescription : tokenResponse.AccessToken);
            return tokenResponse.AccessToken;
        }

        private static async Task<string> GetTokenWithResourceOwnerPassword(string tokenEndpoint)
        {
            var tokenClient = new TokenClient(tokenEndpoint, "pwdClient", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("free", "123456");

            Console.WriteLine(tokenResponse.IsError ? tokenResponse.ErrorDescription : tokenResponse.AccessToken);
            return tokenResponse.AccessToken;
        }

        private static async Task<bool> GetResourceWithToken(string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(accessToken);

            var httpResponseMessage = await httpClient.GetAsync("http://localhost:5001/api/values");
            Console.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync());

            httpClient.Dispose();
            return httpResponseMessage.IsSuccessStatusCode;
        }

        private static void PrintResult(bool isSuccess, string requestType)
        {
            if (isSuccess)
            {
                Console.WriteLine($"{requestType}请求成功。");
            }
            else
            {
                Console.WriteLine($"{requestType}请求失败。");
            }

            Console.WriteLine();
        }
    }
}
