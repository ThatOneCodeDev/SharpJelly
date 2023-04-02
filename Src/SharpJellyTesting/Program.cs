using System;
using SharpJelly;

namespace SharpJellyTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //string? authorizationKey = Environment.GetEnvironmentVariable("_JFKey", EnvironmentVariableTarget.User);
            //string? serverURI = Environment.GetEnvironmentVariable("_JFURI", EnvironmentVariableTarget.User);
            string? authorizationKey = "82f0c035c7d04b0d98a70869b82d5a68"; // Pissenshitter
            string? serverURI = "https://veast.network/jellyfin/";
            if (authorizationKey == null)
            {
                Console.WriteLine("No jellyfin API key was set in environment. Please set a user environment variable with your Jellyfin API key titled: _JFKey");
                Environment.Exit(1);
            }
            if (serverURI == null)
            {
                Console.WriteLine("No jellyfin server URI was set in environment. Please set a user environment variable with your Jellyfin server address titled: _JFURI");
                Environment.Exit(1);
            }
            APIClient apiClient = new(serverURI, authorizationKey);

            if (await apiClient.CheckServerHealthAsync() == "Healthy")
                Console.WriteLine($"Connection to {serverURI} valid and healthy!");

            //var user = SharpJelly.Helpers.JsonHelpers.FindJFUser(await apiClient.ListUsersRawAsync(), "jerhynh");
            //Console.WriteLine(user!.Policy.IsAdministrator);
            //Console.WriteLine("Loading tests...");


            //Console.WriteLine(await apiClient.CreateUserAsync("PissenShit", "Test69!")); 
            //var user = SharpJelly.Helpers.JsonHelpers.FindJFUser(await apiClient.ListUsersRawAsync(), "PissenShit");
            //user!.Policy!.IsDisabled = false;
            //await apiClient.ImposeUserPolicyAsync(user!.Id!, user.Policy);

            Console.WriteLine(await apiClient.DeleteUserAsync("c52326acb6814b8b987bf0eb2345b0f0")); 


        }
    }
}