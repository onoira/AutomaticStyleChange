using HarmonyLib;
using RimWorld;
using Verse;

namespace AutomaticStyleChange
{
    [HarmonyPatch(typeof(Pawn_StyleTracker))]
    [HarmonyPatch(nameof(Pawn_StyleTracker.StyleTrackerTick))]
    public static class Patch_Pawn_StyleTracker_StyleTrackerTick
    {
        // 1 – 2 hours
        private static readonly IntRange Offset = new IntRange(2500, 5000);

        public static bool Prefix(Pawn_StyleTracker __instance, ref bool ___lookChangeDesired)
        {
            if (!__instance.CanDesireLookChange
             || ___lookChangeDesired
             || Find.TickManager.TicksGame < __instance.nextStyleChangeAttemptTick)
                return false;

            if (Rand.MTBEventOccurs(20f, 60_000f, 2500f))
            {
                Debug.Message($"Requesting look change for '{__instance.pawn.Name.ToStringShort}'");
                __instance.RequestLookChange();
            }
            else
            {
                __instance.nextStyleChangeAttemptTick =
                    Find.TickManager.TicksGame + Offset.RandomInRange;
                Debug.Message($"Next StyleTrackerTick for '{__instance.pawn.Name.ToStringShort}' at {__instance.nextStyleChangeAttemptTick}");
            }

            return false;
        }
    }
}