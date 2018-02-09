using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapzip
{
    class ManifestStructure
    {

        public int layoutVersion { get; set; }
        public string timeCreated { get; set; }
        public string timeLastUsed { get; set; }
        public string installerVersionCreated { get; set; }
        public List<Installedmap> installedMaps { get; set; }

        public class Installedmap
        {
            public string timeInstalled { get; set; }
            public string internalName { get; set; }
            public string profileIdentifier { get; set; }
            public string friendlyName { get; set; }
            public string mapVersion { get; set; }
            public string installerVersion { get; set; }
            public bool showWebpage { get; set; }
            public string webpage { get; set; }
        }
    }
}
