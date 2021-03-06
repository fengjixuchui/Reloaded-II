﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Loader.IO.Interfaces;
using Reloaded.Mod.Loader.IO.Utility;
using Reloaded.Mod.Loader.IO.Weaving;

namespace Reloaded.Mod.Loader.IO.Config
{
    public class ModConfig : ObservableObject, IModConfig, IConfig, IConfigCleanup
    {
        /// <summary>
        /// The name of the configuration file as stored on disk.
        /// </summary>
        public const string ConfigFileName = "ModConfig.json";

        public string ModId             { get; set; } = "reloaded.template.modconfig";
        public string ModName           { get; set; } = "Reloaded Mod Config Template";
        public string ModAuthor         { get; set; } = "Someone";
        public string ModVersion        { get; set; } = "1.0.0";
        public string ModDescription    { get; set; } = "Template for a Reloaded Mod Configuration";
        public string ModDll            { get; set; } = "";
        public string ModImage          { get; set; } = "";
        public string[] ModDependencies { get; set; } = new string[0];


        /*
           ---------
           Utilities
           --------- 
        */

        /// <summary>
        /// Writes the configuration to a specified file path.
        /// </summary>
        public static void WriteConfiguration(string path, ModConfig config)
        {
            var _applicationConfigLoader = new ConfigLoader<ModConfig>();
            _applicationConfigLoader.WriteConfiguration(path, config);
        }

        /*
           ---------
           Overrides
           ---------
        */

        /* Useful for debugging. */
        public override string ToString()
        {
            return $"ModId: {ModId}, ModName: {ModName}";
        }

        public void CleanupConfig(string thisPath)
        {
            if (String.IsNullOrEmpty(ModName))
                ModName = "Reloaded Application Name";

            if (String.IsNullOrEmpty(ModId))
                ModId = ModName.Replace(" ", ".");

            if (!String.IsNullOrEmpty(thisPath))
            {
                string imagePath = Path.Combine(Path.GetDirectoryName(thisPath), ModImage);
                if (!File.Exists(imagePath))
                    ModImage = "";
            }

            ModDependencies = ConfigCleanupUtility.FilterNonexistingModIds(ModDependencies).ToArray();
        }

        /*
           ------------------------
           Overrides: Autogenerated
           ------------------------
        */

        protected bool Equals(ModConfig other)
        {
            return string.Equals(ModId, other.ModId) && 
                   string.Equals(ModName, other.ModName) && 
                   string.Equals(ModAuthor, other.ModAuthor) && 
                   string.Equals(ModVersion, other.ModVersion) && 
                   string.Equals(ModDescription, other.ModDescription) && 
                   string.Equals(ModDll, other.ModDll) && 
                   Enumerable.SequenceEqual(ModDependencies, other.ModDependencies);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return Equals((ModConfig)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ModId != null ? ModId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModName != null ? ModName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModAuthor != null ? ModAuthor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModVersion != null ? ModVersion.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModDescription != null ? ModDescription.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModDll != null ? ModDll.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ModDependencies != null ? ModDependencies.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
