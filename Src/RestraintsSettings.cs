using Verse;

namespace Restraints
{
    public class RestraintsSettings : ModSettings
    {
        public static float RestraintBerserkChance = 0.3f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref RestraintBerserkChance, "RestraintBerserkChance", 0.3f);

        }
    }
}
