using HarmonyLib;
using UnityEngine;
using NoSmokeSimplified;
using System;

/* 
 * Class Purpose and Functions
 * 
 * Patches the UpdateSmoke function of smelters
 * Checks to see if smoke is turned on in the config.
 * Checks to see if a smokespawn exists because WindMills, Spinning Wheels are considered smelters in the game and will throw an Null Exception if checking for smoke
 * Turns on an off smoke as configured
 * Checks if smelter is blocked and tells the game it is not.  This allows smelter stacking
 * 
 */

namespace NoSmokeSimplified.Patches;

    internal class SmelterPatch
    {
    
        [HarmonyPatch(typeof(Smelter), "UpdateSmoke")]
        private class SmelterUpdateSmoke_Patch
        {
            private static void Postfix(Smelter __instance)

            {
                try
                {
                    if (Case.ConfigCheckGiveMeSmoke(__instance.name))
                    {
                        if (__instance.m_smokeSpawner != null)
                        {
                            //turns on smoke

                            __instance.m_smokeSpawner.enabled = true;

                            return;
                        }
                    }
                    else

                    {
                        if (__instance.m_smokeSpawner != null)
                        {
                            //turns off smoke and blocking

                            __instance.m_smokeSpawner.enabled = false;
                            __instance.m_blockedSmoke = false;
                            return;
                        }
                    }

                }
            catch (Exception e)
            { NoSmokeSimplifiedMain.TastyLogger.LogWarning(NoSmokeSimplifiedMain.ModName + e); }
        }
        }
    }
