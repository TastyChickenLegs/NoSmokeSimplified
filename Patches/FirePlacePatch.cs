using System;
using UnityEngine;
using HarmonyLib;

/*
 * Class Purpose and Functions
 *
 * Patches the IsBurning routine of the game.
 * Checks to see if smoke is enabled for the particular item If smoke is enabled it then checks if item has a smoke spawner and if so, then checks to see if it is a brazier
 * Check to see if smoke is turned off and if so chechs for the smokespawner and then checks for braziers
 * Turns off or on the smoke
 * Returns is back to the game for native fuel checks and to remain compatible with mods that autofuel
 *
 */

namespace NoSmokeSimplified.Patches;

    internal class FirePlacePatch
    {
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
   
        private class FireplaceIsBurning_Patch
        {
            private static void Postfix(Fireplace __instance, ref bool __result,
                     ref GameObject ___m_enabledObjectHigh)
            {
            try
            {
                //checks if smoke is turned on in config

                if (Case.ConfigCheckGiveMeSmoke(__instance.name))
                {
                    if (__instance.m_smokeSpawner != null)
                        __instance.m_smokeSpawner.enabled = true;
                    if (__instance.name.StartsWith("piece_brazier"))
                        Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.
                                            GetComponent<SmokeSpawner>().enabled = true;
                    if (NoSmokeSimplifiedMain.keepOnInRain.Value) __instance.m_wet = false;

                }
                else
                {
                    if (__instance.m_smokeSpawner != null)

                    {  //check if wet and keep on in rain is true
                       //turns off the smoke and keeps the fire lit in the rain
                        __instance.m_smokeSpawner.enabled = false;
                        if (NoSmokeSimplifiedMain.keepOnInRain.Value) __instance.m_wet = false;

                    }
                    //checks if brazier and stops the smoke, keeps it lit in the rain and returns true

                    if (__instance.name.StartsWith("piece_brazier"))
                    {
                        Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.
                            GetComponent<SmokeSpawner>().enabled = false;
                        //check if wet and keep on in rain is true
                        if (NoSmokeSimplifiedMain.keepOnInRain.Value) __instance.m_wet = false;

                    }
                }
                if (NoSmokeSimplifiedMain.keepOnInRain.Value) __instance.m_wet = false;


                // Return back to game

                float liquidLevel = Floating.GetLiquidLevel(__instance.m_enabledObject.transform.position,
                    1f, LiquidType.All);
                __result = __instance.m_enabledObject.transform.position.y >=
                    liquidLevel && __instance.m_nview.GetZDO().GetFloat("fuel", 0f) > 0f;
            }
            catch (Exception e)
            { NoSmokeSimplifiedMain.TastyLogger.LogWarning(NoSmokeSimplifiedMain.ModName + e); }
            }
        }
    }