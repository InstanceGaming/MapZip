using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace mapzip
{
    public class JsonInterface
    {
        /// <summary>
        /// Edit the profiles json structure with new profile and change the currently selected profile to the new one.
        /// </summary>
        /// <param name="rawJson">the launcher_profiles.json file</param>
        /// <param name="profileName">name of the profile</param>
        /// <param name="data">the properties of the new profile</param>
        /// <returns>editted profiles json structure</returns>
        public static string AddLauncherProfile(string rawJson, string profileName, List<KeyValuePair<string, string>> data)
        {
            dynamic structure = JObject.Parse(rawJson);
            JObject profilesRoot = (JObject)structure[Utils.GetSchemaValue("prof_profiles")];
            JObject selectsRoot = (JObject)structure[Utils.GetSchemaValue("prof_selected_user")];
            JObject newProfile = new JObject();
            JProperty newProfileProps = new JProperty(profileName, newProfile);
            foreach (KeyValuePair<string, string> pair in data)
            {
                newProfile.Add(new JProperty(pair.Key, pair.Value));
            }
            profilesRoot.Add(newProfileProps);
            selectsRoot[JsonSchema.Schema["prof_selected_profile"]] = profileName;
            return SerializeFromStructure(structure, true);
        }
        /// <summary>
        /// Edit the manifest of installed maps and add a new map to the array.
        /// </summary>
        /// <param name="rawJson">the raw json string</param>
        /// <param name="data">the list of properties to add to the new map</param>
        /// <returns>edited json string</returns>
        public static string AddManifestMap(string rawJson, List<KeyValuePair<string, object>> data)
        {
            dynamic structure = JObject.Parse(rawJson);
            JArray installedRoot = (JArray)structure[JsonSchema.Schema["manf_installed"]];
            JObject newMapProps = new JObject();
            foreach (KeyValuePair<string, object> pair in data)
            {
                newMapProps.Add(new JProperty(pair.Key, pair.Value));
            }
            installedRoot.Add(newMapProps);
            return SerializeFromStructure(structure, true);
        }
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
                installedMaps = new ManifestStructure.Installedmap[0],
                uninstalledMaps = new ManifestStructure.Uninstalledmap[0]
            };
            return SerializeFromStructure(manifest,true);
        }

        /// <summary>
        /// Remove root node child from structure.
        /// </summary>
        /// <param name="rawJson">the raw json text</param>
        /// <param name="data">the properties of the new node</param>
        /// <returns>editted json structure</returns>
        public static string AddRootChild(string rawJson, string rootNodeName, string name, List<KeyValuePair<string, string>> data)
        {
            dynamic structure = JObject.Parse(rawJson);
            JObject rootNode = (JObject)structure[rootNodeName];
            //new node object
            JObject newNode = new JObject();
            JProperty newNodeProps = new JProperty(name, newNode);
            //add each pair into json structure
            foreach (KeyValuePair<string, string> pair in data)
            {
                newNode.Add(new JProperty(pair.Key, pair.Value));
            }
            //add node
            rootNode.Add(newNodeProps);
            return SerializeFromStructure(structure, true);
        }

        /// <summary>
        /// Remove root node (of array type) child from structure.
        /// </summary>
        /// <param name="rawJson">the raw json text</param>
        /// <param name="data">the properties of the new node</param>
        /// <returns>editted json structure</returns>
        public static string RemoveRootArrayChild(string rawJson, string rootNodeName, string propName)
        {
            dynamic structure = JObject.Parse(rawJson);
            JArray rootNode = (JArray)structure[rootNodeName];
            //remove node
            foreach (JObject item in rootNode)
            {
                foreach (JProperty prop in item.OfType<JProperty>().ToList())
                {
                    if (prop.Name == propName)
                    {
                        item.Remove();
                    }
                }
            }
            return SerializeFromStructure(structure, true);
        }

        /// <summary>
        /// Remove root node (of object type) child from structure.
        /// </summary>
        /// <param name="rawJson">the raw json text</param>
        /// <param name="data">the properties of the new node</param>
        /// <returns>editted json structure</returns>
        public static string RemoveRootObjectChildProperty(string rawJson, string rootNodeName, string propName)
        {
            dynamic structure = JObject.Parse(rawJson);
            JObject rootNode = (JObject)structure[rootNodeName];
            //remove node
            foreach (JProperty item in rootNode.OfType<JProperty>().ToList())
            {
                if (item.Name == propName)
                {
                    item.Remove();
                }
            }
            return SerializeFromStructure(structure, true);
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
