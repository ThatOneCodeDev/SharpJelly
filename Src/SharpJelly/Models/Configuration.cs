namespace SharpJelly.Models
{
    public class Configuration
    {
        public string? AudioLanguagePreference { get; set; }
        public bool PlayDefaultAudioTrack { get; set; }
        public string? SubtitleLanguagePreference { get; set; }
        public bool DisplayMissingEpisodes { get; set; }
        public List<string>? GroupedFolders { get; set; }
        public string? SubtitleMode { get; set; }
        public bool DisplayCollectionsView { get; set; }
        public bool EnableLocalPassword { get; set; }
        public List<string>? OrderedViews { get; set; }
        public List<string>? LatestItemsExcludes { get; set; }
        public List<string>? MyMediaExcludes { get; set; }
        public bool HidePlayedInLatest { get; set; }
        public bool RememberAudioSelections { get; set; }
        public bool RememberSubtitleSelections { get; set; }
        public bool EnableNextEpisodeAutoPlay { get; set; }
    }
}
