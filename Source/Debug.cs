using Verse;

namespace AutomaticStyleChange
{
    public static class Debug
    {
        public static void Message(string s)
        {
            if (AutomaticStyleChangeMod.Settings.EnableLogging)
                Log.Message($"[{AutomaticStyleChangeMod.PACKAGE_NAME}] {s}");
        }
    }
}