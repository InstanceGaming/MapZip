namespace mapzip
{
    public class ConfigStructure
    {
        public int layoutVersion { get; set; }
        public string[] authors { get; set; }
        public string friendlyName { get; set; }
        public string gameVersion { get; set; }
        public string mapVersion { get; set; }
        public string internalName { get; set; }
        public string profileIcon { get; set; }
        public bool showWebpages { get; set; }
        public string webpageInstalled { get; set; }
        public string webpageUninstalled { get; set; }
    }
}
