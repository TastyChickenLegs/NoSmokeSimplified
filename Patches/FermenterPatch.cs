//using HarmonyLib;
//using System;
//using UnityEngine;

//namespace NoSmokeSimplified.Patches;

//internal class FermenterPatch
//{
//    [HarmonyPatch(typeof(Fermenter), nameof(Fermenter.SlowUpdate))]
//    private class fermenter_smokepatch
//    {
//        private static void Postfix(Fermenter __instance)
//        {
//            __instance.UpdateCover(2f);
//            switch (__instance.GetStatus())
//            {
//                case Fermenter.Status.Empty:
//                    __instance.m_fermentingObject.SetActive(false);
//                    __instance.m_readyObject.SetActive(false);
//                    __instance.m_topObject.SetActive(false);
//                    return;
//                case Fermenter.Status.Fermenting:
//                    __instance.m_readyObject.SetActive(false);
//                    __instance.m_topObject.SetActive(true);
//                    __instance.m_fermentingObject.SetActive(!__instance.m_exposed);
//                    Utils.FindChild(__instance.transform, "smoke").gameObject.GetComponent<Smoke>().enabled = false;
//                    return;
//                case Fermenter.Status.Exposed:
//                    break;
//                case Fermenter.Status.Ready:
//                    __instance.m_fermentingObject.SetActive(false);
//                    __instance.m_readyObject.SetActive(true);
//                    __instance.m_topObject.SetActive(true);
//                    break;
//                default:
//                    return;
//            }
            
            
//        }


//    }
//}
