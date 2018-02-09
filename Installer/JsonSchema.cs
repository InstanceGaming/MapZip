using System.Collections.Generic;

namespace mapzip
{
    static class JsonSchema
    {
        public static Dictionary<string, string> Schema = new Dictionary<string, string>()
        {
            //profile root nodes
            {"prof_profiles","profiles"},
            {"prof_selected_user","selectedUser"},
            //profile names
            {"prof_name", "name"},
            {"prof_type", "type"},
            {"prof_created", "created"},
            {"prof_last_used", "lastUsed"},
            {"prof_icon", "icon"},
            {"prof_game_version", "lastVersionId"},
            {"prof_game_directory", "gameDir"},
            //profile selected user
            {"prof_selected_profile","profile"},
            //config names
            {"conf_version", "version"},
            {"conf_authors", "authors"},
            {"conf_friendly_name", "friendlyName"},
            {"conf_name", "internalName"},
            {"conf_game_version", "gameVersion"},
            {"conf_map_version", "mapVersion"},
            {"conf_icon", "profileIcon"},
            {"conf_show_webpages", "showWebpages"},
            {"conf_webpage_installed", "webpageInstalled"},
            {"conf_webpage_uninstalled", "webpageUninstalled"}
        };
    }
}
