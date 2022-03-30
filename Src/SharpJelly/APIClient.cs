using SharpJelly.Models;
using System.Text;
using System.Text.Json;


namespace SharpJelly
{
    /// <summary>
    /// APIClient contains all externally callable methods used to interact with the Jellyfin API.
    /// </summary>
    public class APIClient
    {
        /// <summary>
        /// The address or "URI" of the server.
        /// </summary>
        public string ServerURI { get; set; }

        /// <summary>
        /// The APIToken or authorization token supplied by the server to allow for use of elevated API endpoints.
        /// </summary>
        public string APIToken { get; set; }

        /// <summary>
        /// Default ctor for the APIClient class.
        /// </summary>
        /// <param name="ServerURI"></param>
        /// <param name="APIToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public APIClient(string ServerURI, string APIToken)
        {


            this.ServerURI = ServerURI ?? throw new ArgumentNullException(nameof(ServerURI)); ;
            this.APIToken = APIToken ?? throw new ArgumentNullException(nameof(APIToken));
            if (this.ServerURI.EndsWith("/"))
                this.ServerURI = this.ServerURI[..(ServerURI.Length - 1)];
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

        /// <summary>
        /// Registers a new JFuser on the server.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns>A json response payload representing th created JFUser.</returns>
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

        /// <summary>
        /// Deletes a JF user from the server.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>204 response or Json payload detailing the user deletion failure.</returns>
        public async Task<string> DeleteUserAsync(string userID)
        {
            using var client = new HttpClient();
            var url = $"{ServerURI}/Users/{userID}";
            client.DefaultRequestHeaders.Add("X-Emby-Token", APIToken);
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Applies a modified user policy to the specified JFUser by userID.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="policy"></param>
        /// <returns>204 response or Json payload detailing the reason why the policy update failed.</returns>
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
        public async Task<string> ListUsersRawAsync() //We shouldn't be returning raw json responses in an API wrapper.
                                                      //ToDo: Rewrite method to enumerate all returned user entries and return a list object of JFUser objects.
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