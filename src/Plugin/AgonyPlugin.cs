using System;
using BepInEx;
using AgonySFX.Patches;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace AgonySFX
{
    [BepInPlugin("com.inory.agonysfx", "inory-agonysfx", "1.1.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> AgonySoundChance;
        internal static ConfigEntry<bool> EnableFikaHeadshotVocalizePatch;
        internal static ConfigEntry<bool> EnableFikaSoundPatch;
        internal static ManualLogSource agonySFXLogger;

        private void Awake()
        {
            agonySFXLogger = BepInEx.Logging.Logger.CreateLogSource("inory-AgonySFX");

            // Config for the chance to play death sound
            AgonySoundChance = Config.Bind("", "Agony Sound Chance", 0.5f,
                new ConfigDescription("Chance of playing the agony sound effects",
                new AcceptableValueRange<float>(0f, 1f)));

            // Config for enabling/disabling FikaHeadshotVocalizePatch
            EnableFikaHeadshotVocalizePatch = Config.Bind("Fika Users Only", "Enable Fika Headshot Vocalize Patch", false,
                new ConfigDescription("**TEMPORARY**: Enable if headshots still causes death sounds to play."));

            // Config for enabling/disabling FikaSoundPatch
            EnableFikaSoundPatch = Config.Bind("Fika Users Only", "Enable Fika Sound Patch", false,
                new ConfigDescription("**TEMPORARY**: Enable if you notice death sounds cutting off before finishing. Disable once Fika updates"));

            agonySFXLogger.LogInfo("Agony SFX Plugin loaded. Applying patches...");
            new DeathSoundPatch().Enable();
            agonySFXLogger.LogInfo("DeathSoundPatch applied.");

            if (EnableFikaHeadshotVocalizePatch.Value)
            {
                agonySFXLogger.LogInfo("Fika Headshot Vocalize Patch is enabled. Applying patch...");
                new HeadshotVocalizePatch().Enable();
            }
            else
            {
                agonySFXLogger.LogInfo("Fika Headshot Vocalize Patch is disabled.");
            }

            if (EnableFikaSoundPatch.Value)
            {
                agonySFXLogger.LogInfo("Fika Sound Patch is enabled. Applying patch...");
                new FikaSoundPatch().Enable();
            }
            else
            {
                agonySFXLogger.LogInfo("Fika Sound Patch is disabled.");
            }
        }
    }
}
