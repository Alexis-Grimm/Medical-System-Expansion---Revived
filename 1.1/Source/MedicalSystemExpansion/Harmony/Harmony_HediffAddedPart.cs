﻿using HarmonyLib;
using Verse;

namespace OrenoMSE.HarmonyPatches
{
    public class Harmony_HediffAddedPart
    {
        [HarmonyPatch(typeof(Hediff_AddedPart))]
        [HarmonyPatch("PostAdd")]
        internal class HediffAddedPart_PostAdd
        {
            [HarmonyPostfix]
            [HarmonyPriority(Priority.First)]
            private static void PartSystem(Hediff_AddedPart __instance, DamageInfo? dinfo)
            {
                HediffComp_PartSystem partSystem = __instance.TryGetComp<HediffComp_PartSystem>();
                if (partSystem != null)
                {
                    partSystem.SpecialPostAdd(dinfo); // TODO: can possibly be factored out by overriding CompPostPostAdd
                }
            }

            [HarmonyPostfix]
            [HarmonyPriority(Priority.Last)]
            private static void AdditionalHediff(Hediff_AddedPart __instance)
            {
                MSE_VanillaExtender.HediffApplyExtraHediffs(__instance, __instance.pawn, __instance.Part);
            }
        }
    }
}
