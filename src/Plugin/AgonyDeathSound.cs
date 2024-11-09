using System;
using BepInEx;
using DeathSoundPatch;

namespace VoiceAdd
{
	[BepInPlugin("com.inory.agonysfx", "inory-agonysfx", "1.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		private void Awake()
		{
			new DeathSoundPatch.DeathSoundPatch().Enable();
		}
	}
}
