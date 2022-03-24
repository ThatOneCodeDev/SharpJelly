using SharpJelly.Models;
using System.Text;
using System.Text.Json;

/// <summary>
/// Please note that this API is in an early development phase and is only a mere prototype at this time.
/// Namespace and class structures are prone to change at any time while early development is underway. -Jerhynh
/// </summary>
namespace SharpJelly
{
    public class APIClient
    {
        public string ServerURI { get; set; }
        public string APIToken { get; set; }
        public APIClient(string ServerURI, string APIToken)
        {
            // Ensure arguments aren't null, next dotnet version implements a cleaner way to do this without declaring these.
            if (ServerURI == null)
                throw new ArgumentNullException($"{nameof(this.ServerURI)} argument values cannot be null.");
            if (APIToken == null)
                throw new ArgumentNullException($"{nameof(this.APIToken)} argument values cannot be null.");

            if (ServerURI.EndsWith("/")) 
                ServerURI = ServerURI.Substring(0, ServerURI.Length - 1);

            this.ServerURI = ServerURI;
            this.APIToken = APIToken;
        }


        /// <summary>
        /// Querys the server's health endpoint. (Note): It is reccomended to call this upon startup to validate configuration.
        /// </summary>
        /// <returns>A response indicating whether the server is healthy or not.</returns>
        /// <exception cref="HttpRequestException">The server replied to the request with a non-success status code.</exception>
        public async Task<string> CheckServerHealthAsync()
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/health";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
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

        /// <summary>
        /// Querys the server for a list of all user accounts registered on the Jellyfin server in context.
        /// </summary>
        /// <returns>A json formatted string containing all registered Jellyfin users and associated configurations.</returns>
        /// <exception cref="HttpRequestException">The server replied to the request with a non-success status code.</exception>
        public async Task<string> ListUsersRawAsync()
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Searches the supplied payload that can be obtained by calling the ListUsersRawAsync method and attempts to find the specified user.
        /// </summary>
        /// <param name="listUsersResult"></param>
        /// <param name="targetUser"></param>
        /// <returns>JFUser object representing a Jellyfin user, or null if the user wasn't found.</returns>
        public static JFUser? JFUserSearcher(string listUsersResult, string targetUser)
        {
            var JsonObject = JsonDocument.Parse(listUsersResult);
            foreach (var array in JsonObject.RootElement.EnumerateArray())// loop each object contained in root array.
            {
                foreach (var obj in array.EnumerateObject())// loop each element contained within the object in context.
                {
                    if (obj.Name == "Name")
                    {
                        if (obj.Value.GetString() == targetUser)
                        {
                            // Found our user!
                            return JsonSerializer.Deserialize<JFUser>(array);
                        }
                    }
                }
            }
            return null;
        }
    }
}