﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mapzip.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MapZip Installer")]
        public string ProductFriendlyName {
            get {
                return ((string)(this["ProductFriendlyName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{0} (v{1}) by Jacob Jewett.")]
        public string Text_Welcome {
            get {
                return ((string)(this["Text_Welcome"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Reading configuration...")]
        public string Text_ReadingConfig {
            get {
                return ((string)(this["Text_ReadingConfig"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Licenced under GNU General Public Licence v3.0")]
        public string Text_Licence {
            get {
                return ((string)(this["Text_Licence"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Config file empty or syntax malformed; Please report this to the mapmaker. Raw me" +
            "ssage: ")]
        public string Text_ErrConfigSyntax {
            get {
                return ((string)(this["Text_ErrConfigSyntax"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Config file version mismatch. Application version {0}, config verision {1}. Pleas" +
            "e report this to the mapmaker.")]
        public string Text_ErrConfigVersionMismatch {
            get {
                return ((string)(this["Text_ErrConfigVersionMismatch"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Config file parsed with no errors.")]
        public string Text_ConfigReadSuccess {
            get {
                return ((string)(this["Text_ConfigReadSuccess"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Licence Agreement")]
        public string Text_LicenceAgreementHeader {
            get {
                return ((string)(this["Text_LicenceAgreementHeader"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Are you sure you want to install the map \"{0}\" by {1} (v{2})? (Y or N)")]
        public string Text_InstallConfirmation {
            get {
                return ((string)(this["Text_InstallConfirmation"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Tip: Save your game as it will be closed forcibly if not exited now.")]
        public string Text_InstallNoticeGameSave {
            get {
                return ((string)(this["Text_InstallNoticeGameSave"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Thanks for using {0} v{1}! Press any key to exit...")]
        public string Text_Thanks {
            get {
                return ((string)(this["Text_Thanks"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MAPZIP,ERROR,CONFIG,INSTALLER,IO,AUTHENTICATION,CLI")]
        public string Text_Tags {
            get {
                return ((string)(this["Text_Tags"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("------------------------------------------------------------")]
        public string Text_Separator {
            get {
                return ((string)(this["Text_Separator"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("The profile data zip was not found. Please report this to the mapmaker.")]
        public string Text_ErrProfileZipNotFound {
            get {
                return ((string)(this["Text_ErrProfileZipNotFound"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Argument parsing failed, errors returned. Raw message:")]
        public string Text_ErrArgumentErrors {
            get {
                return ((string)(this["Text_ErrArgumentErrors"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("This feature is not implemented yet.")]
        public string Text_NotImplemented {
            get {
                return ((string)(this["Text_NotImplemented"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"Usage:@[--help]: Displays this help text.@[-c,--config]: Specify a custom config file path.@[-d,--data]: Specify a custom profile data path.@[-s,--silent]: Dont print console messages and bypass confirmation.@[-u,--uninstall]: Uninstall the map this installer unpacks.")]
        public string Text_ArgumentsHelpText {
            get {
                return ((string)(this["Text_ArgumentsHelpText"]));
            }
        }
    }
}
