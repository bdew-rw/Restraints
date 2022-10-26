using HarmonyLib;
using RimWorld;
using Verse;

namespace Restraints
{
    public class RestraintsMod : Mod
    {
        public RestraintsMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("BDew.Restraints");
            harmony.PatchAll();
            Log.Message("Restraints loaded");
        }
    }
}