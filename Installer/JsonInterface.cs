using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace mapzip
{
    public class JsonInterface
    {
        /// <summary>
        /// Create a blank manifest json file.
        /// </summary>
        /// <param name="lyversion">layout version number</param>
        /// <param name="created">the universal date that the manifest was made at</param>
        /// <param name="version">the installer version that created the manifest</param>
        /// <returns>new json string</returns>
        public static string CreateBlankManifest(int lyversion, string created, string version)
        {
            ManifestStructure manifest = new ManifestStructure() {

                layoutVersion = lyversion,
                timeCreated = created,
                timeLastUsed = created,
                installerVersionCreated = version,
                installedMaps = new List<ManifestStructure.Installedmap>()
            };
            return SerializeFromStructure(manifest,true);
        }

        /// <summary>
        /// Take abstracted object and convert it into JSON.
        /// </summary>
        /// <param name="structure">the class structure</param>
        /// <param name="indented">make the json indented</param>
        /// <returns>the formatted json</returns>
        public static string SerializeFromStructure(object structure, bool indented)
        {
            return JsonConvert.SerializeObject(structure, indented ? Formatting.Indented : Formatting.None);
        }

        /// <summary>
        /// Take text JSON and convert it into a abstracted object.
        /// </summary>
        /// <typeparam name="GenericJsonStructure">the structure to attempt and fit the data into</typeparam>
        /// <param name="rawJson">the raw json to parse</param>
        /// <returns>the abstract structure with values</returns>
        public static GenericJsonStructure DeserializeToStructure<GenericJsonStructure>(string rawJson)
        {
            return JsonConvert.DeserializeObject<GenericJsonStructure>(rawJson);
        }
    }
}
