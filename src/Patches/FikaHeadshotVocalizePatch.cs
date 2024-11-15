using EFT;
using System;
using System.Reflection;
using SPT.Reflection.Patching;
using HarmonyLib;
using Fika.Core.Coop.Players;

namespace AgonySFX.Patches
{
    public class HeadshotVocalizePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ObservedCoopPlayer).GetMethod("ShouldVocalizeDeath", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        [PatchPrefix]

        public static bool Prefix(ObservedCoopPlayer __instance, EBodyPart bodyPart)
        {
            return __instance.LastDamagedBodyPart > EBodyPart.Head;
        }
    }
}
