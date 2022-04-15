using SharpJelly.Models;
using System.Text.Json;

namespace SharpJelly.Helpers
{
    public class JsonHelpers
    {
        public static JFUser? FindJFUser(string payload, string username)
        {
            var JsonObject = JsonDocument.Parse(payload);
            foreach (var array in JsonObject.RootElement.EnumerateArray())// loop each object contained in root array.
            {
                foreach (var obj in array.EnumerateObject())// loop each element contained within the object in context.
                {
                    if (obj.Name == "Name")
                    {
                        if (obj.Value.GetString() == username)
                        {
                            return JsonSerializer.Deserialize<JFUser>(array);
                        }
                    }
                }
            }
            return null;
        }


    }
}
