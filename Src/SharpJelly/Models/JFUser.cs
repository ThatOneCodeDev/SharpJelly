namespace SharpJelly.Models
{
    public class JFUser
    {
        public JFUser()
        {
            Configuration = new();
            Policy = new();
        }

        public string? Name { get; set; }
        public string? ServerId { get; set; }
        public string? ServerName { get; set; }
        public string? Id { get; set; }
        public string? PrimaryImageTag { get; set; }
        public bool HasPassword { get; set; }
        public bool HasConfiguredPassword { get; set; }
        public bool HasConfiguredEasyPassword { get; set; }
        public bool EnableAutoLogin { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public Configuration? Configuration { get; set; }
        public Policy? Policy { get; set; }
        public int PrimaryImageAspectRatio { get; set; }
    }
}

