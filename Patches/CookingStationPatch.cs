//using HarmonyLib;

/////*
//// * Purpose and Function
//// *
//// * Patch the cooking station routing of the game for ovens
//// * run the native routine for the game
//// * at the end look to see if smoke is enabled for the ove
//// * find the child smokespawner using the game utils
//// * see if game expects the fire to be lit
//// * turn of smoke spawner if configured to be off
//// *
//// */

//namespace NoSmokeSimplified.Patches;

//internal class CookingStationPatch
//{
//    [HarmonyPatch(typeof(CookingStation), nameof(CookingStation.GetFuel))]
//    private class CookingStationSmoke_Patch

//    {
//        //native functions of game
//        private static float Postfix(CookingStation __instance)
//        {
//            //if (__instance.m_fireCheckPoints != null && __instance.m_fireCheckPoints.Length != 0)

//            //{
//            //    Transform[] fireCheckPoints = __instance.m_fireCheckPoints;
//            //    for (int i = 0; i < fireCheckPoints.Length; i++)
//            //    {
//            //        if (!EffectArea.IsPointInsideArea(fireCheckPoints[i].position, EffectArea.Type.Burning, __instance.m_fireCheckRadius))
//            //        {
//            //            __result = false;
//            //            return;
//            //        }
//            //    }
//            //    __result = true;
//            //}

//            //__result = EffectArea.IsPointInsideArea(__instance.transform.position, EffectArea.Type.Burning, __instance.m_fireCheckRadius);

//            //check smoke configured

//            //check if fire is lit and turn off/on smoke

//            if (Case.ConfigCheckGiveMeSmoke(__instance.name))
//            {
//                Utils.FindChild(__instance.transform, "CookingStation").gameObject.
//                                                            GetComponent<SmokeSpawner>().enabled = false;
                
//            }
//            else
//                Utils.FindChild(__instance.transform, "CookingStation").gameObject.
//                                    GetComponent<SmokeSpawner>().enabled = true;

//            return  __instance.m_nview.GetZDO().GetFloat("fuel", 0f);
       
//        }
//    }
//}