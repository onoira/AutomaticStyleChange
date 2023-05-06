using HarmonyLib;
using UnityEngine;
using Verse;

namespace AutomaticStyleChange
{
    public class AutomaticStyleChangeMod : Mod
    {
        public const string PACKAGE_ID = "onoira.automaticstylechange";
        public const string PACKAGE_NAME = "Automatic Style Change";

        public AutomaticStyleChangeMod(ModContentPack content) : base(content)
        {
            new Harmony(PACKAGE_ID).PatchAll();
            Settings = GetSettings<Settings>();
            Log.Message("[Automatic Style Change] Loaded");
        }

        public override string SettingsCategory() => "Automatic Style Change";
        public override void DoSettingsWindowContents(Rect rect) =>
            Settings.DoWindowContents(rect);

        public static Settings Settings;
    }
}