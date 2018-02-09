using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace mapzip
{
    class LauncherStructure
    {
        public Settings settings { get; set; }
        public Launcherversion launcherVersion { get; set; }
        public Dictionary<string, Profile> profiles { get; set; }
        public Dictionary<string, AuthenticationUser> authenticationDatabase { get; set; }
        public Selecteduser selectedUser { get; set; }
        public string analyticsToken { get; set; }
        public int analyticsFailcount { get; set; }
        public string clientToken { get; set; }

        public class Settings
        {
            public string locale { get; set; }
            public bool showMenu { get; set; }
        }

        public class Launcherversion
        {
            public string name { get; set; }
            public int format { get; set; }
            public int profilesFormat { get; set; }
        }

        public class Selecteduser
        {
            public string account { get; set; }
            public string profile { get; set; }
        }

        public class Profile
        {
            public string name { get; set; }
            public string type { get; set; }
            public string created { get; set; }
            public string lastUsed { get; set; }
            public string icon { get; set; }
            public string gameVersion { get; set; }
            public string gameDirectory { get; set; }
        }

        public class AuthenticationUser
        {
            public string accessToken { get; set; }
            public string username { get; set; }
            public Dictionary<string, Profile> profiles { get; set; }
        }
    }
}
