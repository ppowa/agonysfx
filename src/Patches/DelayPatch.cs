using EFT;
using System;
using System.Reflection;
using System.Collections;
using SPT.Reflection.Patching;
using HarmonyLib;
using Fika.Core.Coop.Players;
using UnityEngine;

namespace AgonySFX.Patches
{
    public class DelayPatch : ModulePatch
    {
        //Workaround Fika's 2 second network cleanup
        //timer by extending it so it doesn't interrupt death sounds
        protected override MethodBase GetTargetMethod()
        {
            return typeof(ObservedCoopPlayer).GetMethod("DestroyNetworkedComponents", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public static float extendWaitTime = 15f; 

        [PatchPrefix]
        public static bool Prefix(ref IEnumerator __result)
        {
            __result = DelayedWaitCoroutine(extendWaitTime);
            return false; 

        }

        private static IEnumerator DelayedWaitCoroutine(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
}
