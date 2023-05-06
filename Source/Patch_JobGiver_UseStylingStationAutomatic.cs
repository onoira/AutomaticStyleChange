using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace AutomaticStyleChange
{
    [HarmonyPatch(typeof(JobGiver_UseStylingStationAutomatic))]
    [HarmonyPatch("TryGiveJob")]
    public static class Patch_JobGiver_UseStylingStationAutomatic
    {
        // 1 – 3 days
        private static readonly IntRange Offset = new IntRange(60_000, 180_000);

        public static bool Prefix(Pawn pawn, ref Job __result)
        {
            if (!ModsConfig.IdeologyActive || Find.TickManager.TicksGame < pawn.style.nextStyleChangeAttemptTick)
                __result = null;

            Debug.Message($"Processing job for {pawn.Name.ToStringShort}");

            Thing thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.StylingStation), PathEndMode.InteractionCell, TraverseParms.For(pawn), 9999f, (Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x));
            if (thing == null)
            {
                pawn.style.nextStyleChangeAttemptTick =
                    Find.TickManager.TicksGame + Offset.RandomInRange;
                Debug.Message($"No available station; next job attempt at {pawn.style.nextStyleChangeAttemptTick}");
                __result = null;
            }

            __result = JobMaker.MakeJob(JobDefOf.UseStylingStationAutomatic, thing);
            return false;
        }
    }
}