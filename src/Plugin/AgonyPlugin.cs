using System;
using BepInEx;
using AgonySFX.Patches;
using BepInEx.Configuration;


namespace AgonySFX
{
	[BepInPlugin("com.inory.agonysfx", "inory-agonysfx", "1.1.0")]
	public class Plugin : BaseUnityPlugin
	{
        internal static ConfigEntry<float> AgonySoundChance;
        private void Awake()
        {
            //Config the chance to play death sound
            AgonySoundChance = Config.Bind("", "Agony Sound Chance", 0.5f,
                new ConfigDescription("Chance of playing the agony sound effects",
                new AcceptableValueRange<float>(0f, 1f)));

            Logger.LogInfo("Agony SFX Plugin loaded. Applying patches...");
            new DeathSoundPatch().Enable();
		}
	}
}
