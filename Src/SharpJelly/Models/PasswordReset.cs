namespace SharpJelly
{
    /// <summary>
    /// PasswordReset model representing a payload for sending password reset requests uo user endpoints.
    /// </summary>
    public class PasswordReset
    {
        /// <summary>
        /// Gets or sets the current sha1-hashed password.
        /// </summary>
        public string? CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the current plain text password.
        /// </summary>
        public string? CurrentPw { get; set; }

        /// <summary>
        /// Gets or sets the new plain text password.
        /// </summary>
        public string? NewPw { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to reset the password.
        /// </summary>
        public bool ResetPassword { get; set; }
    }


}
