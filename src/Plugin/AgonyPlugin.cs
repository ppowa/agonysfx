using System;
using BepInEx;
using AgonySFX.Patches;
using BepInEx.Configuration;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;

namespace AgonySFX
{
    [BepInPlugin("com.inory.agonysfx", "inory-agonysfx", "1.1.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<float> AgonySoundChance;
        internal static ManualLogSource agonySFXLogger;

        private void Awake()
        {
            agonySFXLogger = BepInEx.Logging.Logger.CreateLogSource("inory-AgonySFX");

            // Config the chance to play death sound
            AgonySoundChance = Config.Bind("", "Agony Sound Chance", 0.5f,
                new ConfigDescription("Chance of playing the agony sound effects",
                new AcceptableValueRange<float>(0f, 1f)));

            agonySFXLogger.LogInfo("Agony SFX Plugin loaded. Checking for Fika.Core...");
                new DeathSoundPatch().Enable();

            // Fika only patch
            if (IsFikaCoreAvailable())
            {
                agonySFXLogger.LogInfo("Fika.Core detected! Applying patches...");
                new DelayPatch().Enable();
                new HeadshotVocalizePatch().Enable();
            }
        }

        private bool IsFikaCoreAvailable()
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var fikaAssembly = assemblies.FirstOrDefault(asm => asm.GetName().Name == "Fika.Core");

                return fikaAssembly != null;
            }
            catch (Exception ex)
            {
                agonySFXLogger.LogError($"Error while checking for Fika.Core: {ex.Message}");
                return false;
            }
        }
    }
}
