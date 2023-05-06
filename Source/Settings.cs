using UnityEngine;
using Verse;

namespace AutomaticStyleChange
{
    public class Settings : ModSettings
    {
        private const int DayInTicks = 60_000;
        private const int YearInDays = 60;

        public int LimitLower = 900_000;
        public int LimitUpper = 3_600_000;
        public bool EnableLogging;

        private int DaysLower => LimitLower / DayInTicks;
        private int DaysUpper => LimitUpper / DayInTicks;

        private Listing_Standard listingStandard;

        public void DoWindowContents(Rect canvas)
        {
            Rect rect = canvas.ContractedBy(40f);
            listingStandard = new Listing_Standard();
            listingStandard.ColumnWidth = (rect.width - 40f) / 2f;
            listingStandard.Begin(canvas.ContractedBy(60f));

            listingStandard.LabelDouble("Minimum days between changes", DaysLower.ToString() ?? "");
            LimitLower = (int)listingStandard.Slider(DaysLower, 1f, DaysUpper-1) * DayInTicks;

            listingStandard.LabelDouble("Maximum days between changes", DaysUpper.ToString() ?? "");
            LimitUpper = (int)listingStandard.Slider(DaysUpper, DaysLower+1, YearInDays) * DayInTicks;

            listingStandard.CheckboxLabeled("Enable logging", ref EnableLogging);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref LimitLower, "LimitLower", 0);
            Scribe_Values.Look(ref LimitUpper, "LimitUpper", 0);
        }
    }
}