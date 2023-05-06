using HarmonyLib;
using RimWorld;
using Verse;

namespace AutomaticStyleChange
{
    [HarmonyPatch(typeof(Pawn_StyleTracker))]
    [HarmonyPatch(nameof(Pawn_StyleTracker.ResetNextStyleChangeAttemptTick))]
    public static class Patch_Pawn_StyleTracker_ResetNextStyleChangeAttemptTick
    {
        public static bool Prefix(Pawn_StyleTracker __instance)
        {
            __instance.nextStyleChangeAttemptTick =
                Find.TickManager.TicksGame + new IntRange(
                    AutomaticStyleChangeMod.Settings.LimitLower,
                    AutomaticStyleChangeMod.Settings.LimitUpper
                ).RandomInRange;
            Debug.Message($"Next style change attempt for '{__instance.pawn.Name.ToStringShort}' at {__instance.nextStyleChangeAttemptTick}");
            return false;
        }
    }
}