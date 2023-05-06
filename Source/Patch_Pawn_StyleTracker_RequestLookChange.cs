using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutomaticStyleChange
{
    [HarmonyPatch(typeof(Pawn_StyleTracker))]
    [HarmonyPatch(nameof(Pawn_StyleTracker.RequestLookChange))]
    public static class Patch_Pawn_StyleTracker_RequestLookChange
    {
        // 1 – 3 days
        private static readonly IntRange Offset = new IntRange(60_000, 180_000);

        public static bool Prefix(Pawn_StyleTracker __instance, ref bool ___lookChangeDesired)
        {
            Find.LetterStack.ReceiveLetter("LetterWantLookChange".Translate() + ": " + Find.ActiveLanguageWorker.WithDefiniteArticle(__instance.pawn.Name.ToStringShort, __instance.pawn.gender, plural: false, name: true), "LetterWantLookChangeDesc".Translate(__instance.pawn.Named("PAWN")), LetterDefOf.NeutralEvent, new LookTargets(__instance.pawn), null, null, new List<ThingDef> { ThingDefOf.StylingStation });
            ___lookChangeDesired = true;
            __instance.nextStyleChangeAttemptTick =
                Find.TickManager.TicksGame + Offset.RandomInRange;
            Debug.Message($"Automatic style change for '{__instance.pawn.Name.ToStringShort}' at {__instance.nextStyleChangeAttemptTick}");
            return false;
        }
    }
}