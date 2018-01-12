using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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
        public static string EditLauncherProfiles(string rawJson, string profileName, List<KeyValuePair<string, string>> data)
        {
            dynamic structure = JObject.Parse(rawJson);
            JObject profilesRoot = (JObject)structure["profiles"];
            JObject selectsRoot = (JObject)structure["selectedUser"];
            JObject newProfile = new JObject();
            JProperty newProfileProps = new JProperty(profileName, newProfile);
            foreach (KeyValuePair<string, string> pair in data)
            {
                newProfile.Add(new JProperty(pair.Key, pair.Value));
            }
            profilesRoot.Add(newProfileProps);
            selectsRoot["profile"] = profileName;
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
