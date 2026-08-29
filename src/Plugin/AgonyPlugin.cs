using System;
using BepInEx;
using AgonySFX.Patches;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace AgonySFX
{
    [BepInDependency("com.SPT.core", "4.1.0")]
    [BepInPlugin("com.inory.agonysfx", "inory-agonysfx", "1.2.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> AgonySoundChance;
        public static ConfigEntry<bool> DebugLogs;
        internal static ManualLogSource agonySFXLogger;

        private void Awake()
        {
            agonySFXLogger = BepInEx.Logging.Logger.CreateLogSource("inory-AgonySFX");

            // Config for the chance to play death sound
            AgonySoundChance = Config.Bind("", "Agony Sound Chance", 0.5f,
                new ConfigDescription("Chance of playing the agony sound effects",
                new AcceptableValueRange<float>(0f, 1f)));

            //Made debug toggleable and off by default to avoid flooding logs.
            DebugLogs = Config.Bind("Debug", "Debug", false, new ConfigDescription("Enable debug logs", null, new ConfigurationManagerAttributes { IsAdvanced = true }));

            agonySFXLogger.LogInfo("Agony SFX Plugin loaded. Applying patches...");
            new DeathSoundPatch().Enable();
            agonySFXLogger.LogInfo("DeathSoundPatch applied.");

        }
    }
}