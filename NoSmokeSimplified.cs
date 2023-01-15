using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;

namespace NoSmokeSimplified
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInProcess("valheim.exe")]
    [BepInIncompatibility("Tastychickenlegs.NoSmokeStayLit")]
    public class NoSmokeSimplifiedMain : BaseUnityPlugin

    {
        /*
         * Removes Smoke and Allows Smelter Stacking
         *
         * Intial Game Settings
         */
        
        private readonly Harmony harmony = new Harmony("Tastychickenlegs.NoSmokeSimplified");
        internal const string ModName = "NoSmokeSimplified";
        internal const string ModVersion = "1.0.0";
        internal const string Author = "TastyChickenLegs";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath +
            Path.DirectorySeparatorChar + ConfigFileName;
        public static readonly ManualLogSource TastyLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);

        //Game Configuration Variables
        public static NoSmokeSimplifiedMain context;
        private static ConfigEntry<bool> _configEnabled = null;
        internal static ConfigEntry<bool> fe_piece_brazierfloor01;
        internal static ConfigEntry<bool> fe_piece_brazierceiling01;
        public static ConfigEntry<bool> fe_fire_pit;
        internal static ConfigEntry<bool> fe_bonfire;
        internal static ConfigEntry<bool> fe_hearth;
        internal static ConfigEntry<bool> fe_smelter;
        internal static ConfigEntry<bool> keepOnInRain;
        //internal static ConfigEntry<bool> fe_oven;

        private void Awake()
        {
            //Create the config File for plugin check if enabled, turn on file watcher and patch the game

            _configEnabled = Config.Bind<bool>("General", "Mod Enabled", defaultValue: true, "Sets the mod to be enabled or not.");
            fe_fire_pit = Config.Bind<bool>("Really, I Want Smoke", "Camp Fire Enable Smoke", false, "Enable eternal Smoke for Bonfire");
            fe_hearth = Config.Bind<bool>("Really, I Want Smoke", "Hearth Enable Smoke", false, "Enable Smoke for Hearth");
            fe_piece_brazierfloor01 = Config.Bind<bool>("Really, I Want Smoke", "Standing Brazier Enable Smoke", false, "Enable for Standing brazier");
            fe_piece_brazierceiling01 = Config.Bind<bool>("Really, I Want Smoke", "Ceiling Brazier Enable Smoke", false, "Enable timer for Hanging brazier");
            fe_smelter = Config.Bind<bool>("Really, I Want Smoke", "Smelter Enable Smoke", false, "Enable Smoke for Smelter.  This disables Smelter Stacking");
            fe_bonfire = Config.Bind<bool>("Really, I Want Smoke", "Bonfire Enable Smoke", false, "Enable Smoke for Bonefire");
            keepOnInRain = Config.Bind<bool>("General", "Keep on in Rain", true, "Keep items from going out in the rain");
           // fe_oven = Config.Bind<bool>("Really, I Want Smoke", "Oven Enable Smoke", false, "Enable Smoke for Cooking Station.");

            if (!_configEnabled.Value)
                return;

            harmony.PatchAll();
            SetupWatcher();
        }

        private void OnDestroy()
        {
            //Save cofig, unload harmony
            Config.Save();

            harmony.UnpatchSelf();
        }

        private void SetupWatcher()
        {
            //Turn on filewatcher to reload config when changes made

            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                TastyLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
                TastyLogger.LogInfo(ModName + "Config Reloaded");
            }
            catch
            {
                TastyLogger.LogError($"There was an issue loading your {ConfigFileName}");
                TastyLogger.LogError("Please check your config entries for spelling and format!");
            }
        }
    }
}