using SharpJelly.Models;
using System.Text;
using System.Text.Json;

namespace SharpJelly
{
    public class APIClient
    {
        public string ServerURI { get; set; }
        public string APIToken { get; set; }
        public APIClient(string ServerURI, string APIToken)
        {
            this.ServerURI = ServerURI;
            this.APIToken = APIToken;
        }

        public async Task<string> CreateUserAsync(string Name, string Password)
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users/New";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            NewUserModel model = new()
            {
                Name = Name,
                Password = Password,
            };
            string jsonPayload = JsonSerializer.Serialize(model);
            var response = await client.PostAsync(url, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteUserAsync(string userID)
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users/{userID}";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ImposeUserPolicyAsync(string userID, Policy policy)
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users/{userID}/Policy";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            string jsonPayload = JsonSerializer.Serialize(policy);
            var response = await client.PostAsync(url, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ListUsersAsync()
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}