using RimWorld;
using Verse;

namespace Restraints
{
    [DefOf]
    public static class RestraintsDefs
    {
        [DefAlias("bdew_restraints")] public static HediffDef RestraintsHediff;
        [DefAlias("bdew_restraints_add")] public static JobDef RestrainJob;
        [DefAlias("bdew_restraints_free")] public static JobDef FreeJob;
        [DefAlias("bdew_restraints_memory")] public static ThoughtDef RestrainsMemory;

        static RestraintsDefs()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RestraintsDefs));
        }
    }
}