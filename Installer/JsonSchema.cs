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
            //manifest names
            {"manf_layout_version", "layoutVersion"},
            {"manf_time_created", "timeCreated"},
            {"manf_time_last_used", "timeLastUsed"},
            {"manf_installer_version_created", "installerVersionCreated"},
            {"manf_installed", "installedMaps"},
            {"manf_uninstalled", "uninstalledMaps"},
            //manifest installer names
            {"manf_time_installed", "timeInstalled"},
            {"manf_internal_name", "internalName"},
            {"manf_profile_id", "profileId"},
            {"manf_friendly_name", "friendlyName"},
            {"manf_installer_version","installerVersion"},
            {"manf_map_version","mapVersion"},
            {"manf_show_webpage","showWebpage"},
            {"manf_webpage_uninstalled","webpageUninstall"},
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
