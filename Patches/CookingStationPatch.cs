using HarmonyLib;

///*
// * Purpose and Function
// *
// * Patch the cooking station routing of the game for ovens
// * run the native routine for the game
// * at the end look to see if smoke is enabled for the ove
// * find the child smokespawner using the game utils
// * see if game expects the fire to be lit
// * turn of smoke spawner if configured to be off
// *
// */

namespace NoSmokeSimplified.Patches;

internal class CookingStationPatch
{
    [HarmonyPatch(typeof(CookingStation), nameof(CookingStation.UpdateVisual))]
    private class CookingStationSmoke_Patch

    {
        //native functions of game
        private static void Postfix(CookingStation __instance, ref bool fireLit)
        {
          
                for (int i = 0; i < __instance.m_slots.Length; i++)
                {
                    string item;
                    float num;
                    CookingStation.Status status;
                    __instance.GetSlot(i, out item, out num, out status);
                    __instance.SetSlotVisual(i, item, fireLit, status);
                }
                if (__instance.m_useFuel)
                {
                    bool active = __instance.GetFuel() > 0f;
                    if (__instance.m_haveFireObject)
                    {
                        __instance.m_haveFireObject.SetActive(fireLit);
                    }
                    if (__instance.m_haveFuelObject)
                    {
                        __instance.m_haveFuelObject.SetActive(active);
                    }
                }
            //patch out the smoke if configured
            if (__instance.name.StartsWith("piece_oven(Clone)"))
            {
                if (Case.ConfigCheckGiveMeSmoke(__instance.name))
                    Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.GetComponent<SmokeSpawner>().enabled = true;
                else
                    Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.GetComponent<SmokeSpawner>().enabled = false;
            }
        }
        
    }
}