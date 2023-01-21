using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSmokeSimplified;

namespace NoSmokeSimplified.Patches;

    internal class Case
    {
        public static bool ConfigCheckGiveMeSmoke(string instanceName)
        {
            bool GiveMeSmoke = false;
            switch (instanceName)
            {
                case "fire_pit(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_fire_pit.Value;
                    break;

                case "hearth(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_hearth.Value;
                    break;

                case "piece_brazierfloor01(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_piece_brazierfloor01.Value;
                    break;

                case "piece_brazierceiling01(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_piece_brazierceiling01.Value;
                    break;

                case "smelter(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_smelter.Value;
                    break;

                case "piece_oven(Clone)":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_oven.Value;
                    break;

            case "fire_pit_planned":
                GiveMeSmoke = NoSmokeSimplifiedMain.fe_fire_pit.Value;
                break;

            case "hearth_planned":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_hearth.Value;
                    break;

                case "piece_brazierfloor01_planned":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_piece_brazierfloor01.Value;
                    break;

                case "piece_brazierceiling01_planned":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_piece_brazierceiling01.Value;
                    break;

                case "smelter_planned":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_smelter.Value;
                    break;

                case "piece_oven_planned":
                    GiveMeSmoke = NoSmokeSimplifiedMain.fe_oven.Value;
                    break;
        }
            return GiveMeSmoke;
        }
    }

