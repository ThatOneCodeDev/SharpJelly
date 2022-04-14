using System;
using SharpJelly;

namespace SharpJellyTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string? authorizationKey = Environment.GetEnvironmentVariable("_JFKey", EnvironmentVariableTarget.User);
            string? serverURI = Environment.GetEnvironmentVariable("_JFURI", EnvironmentVariableTarget.User);
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

            Console.WriteLine("Loading tests...");

            // ToDo: Write tests :p
        }
    }
}