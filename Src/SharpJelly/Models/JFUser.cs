namespace SharpJelly.Models
{
    /// <summary>
    /// Implements a class representing a Jellyfin User, along with subclasses for config and policy
    /// </summary>
    public class JFUser
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public JFUser()
        {
            Configuration = new();
            Policy = new();
        }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string? Name { get; set; }
        
        /// <summary>
        /// ID of the respoding server
        /// </summary>
        public string? ServerId { get; set; }
        
        /// <summary>
        /// Name of the responding server
        /// </summary>
        public string? ServerName { get; set; }
        
        /// <summary>
        /// The ID of the corresponding user
        /// </summary>
        public string? Id { get; set; }
        
        /// <summary>
        /// Tag of the user's profile picture
        /// </summary>
        public string? PrimaryImageTag { get; set; }
        
        /// <summary>
        /// Displays whether the user have a password set
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// Displays whether the user has configured a password
        /// </summary>
        public bool HasConfiguredPassword { get; set; }

        /// <summary>
        /// Displays whether the user has configured an easy password
        /// </summary>
        public bool HasConfiguredEasyPassword { get; set; }

        /// <summary>
        /// Displays whether the user has auto-login enabled
        /// </summary>
        public bool EnableAutoLogin { get; set; }

        /// <summary>
        /// The last date the user logged on to the server
        /// </summary>
        public DateTime LastLoginDate { get; set; }
        
        /// <summary>
        /// The last date the user interacted with the server
        /// </summary>
        public DateTime LastActivityDate { get; set; }
        
        /// <summary>
        /// The corresponding Configuration object for the user in context
        /// </summary>
        public Configuration? Configuration { get; set; }

        /// <summary>
        /// The corresponding Policy object for the user in context
        /// </summary>
        public Policy? Policy { get; set; }

        /// <summary>
        /// The aspect ratio of the user's profile picture.
        /// </summary>
        public int PrimaryImageAspectRatio { get; set; }
    }
}

