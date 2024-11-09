using EFT;
using System;
using System.Reflection;
using SPT.Reflection.Patching;
using UnityEngine;

namespace DeathSoundPatch
{
    public class DeathSoundPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {   
            return typeof(Player).GetMethod("OnDead", BindingFlags.Instance | BindingFlags.Public |  BindingFlags.NonPublic);
        }

        [PatchPrefix]
        public static bool Prefix(ref EFT.Player __instance)
        {
            EPhraseTrigger trigger;
            
            //Non-weapon induced damage type (Bleedout, grenade, etc)
            if (!__instance.LastDamageType.IsWeaponInduced())
            {
                trigger = EPhraseTrigger.OnAgony;
            }
            else
            {
                //Plays either OnAgony or OnDeath sfx
                trigger = UnityEngine.Random.value < 0.5f ? EPhraseTrigger.OnAgony : EPhraseTrigger.OnDeath;
            }

            //Only plays death sounds when bot dies to non-headshot damage
            if (__instance.ShouldVocalizeDeath(__instance.LastDamagedBodyPart))
            {
                TagBank tagBank = __instance.Speaker.Play(trigger, __instance.HealthStatus, true, default(int?));
                if (tagBank != null)
                {
                    TaggedClip taggedClip = tagBank.Match((int)__instance.HealthStatus);
                    float time = taggedClip?.Length ?? 0f;
                }
            }
            else
            {
                __instance.Speaker.Shut();
            }

            return true;
        }

    }

}