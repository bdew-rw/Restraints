using RimWorld;
using Verse;
using Verse.AI;

namespace Restraints
{
    public class RestraintsMenuProvider : FloatMenuOptionProvider
    {
        protected override bool Drafted => false;

        protected override bool Undrafted => true;

        protected override bool Multiselect => false;

        protected override bool RequiresManipulation => true;

        protected override FloatMenuOption GetSingleOptionFor(Pawn clickedPawn, FloatMenuContext context)
        {
            if (clickedPawn.health.hediffSet.hediffs.Exists(h => h.def == RestraintsDefs.RestraintsHediff))
            {
                return new FloatMenuOption("Restraints.Remove".Translate(clickedPawn), () => RunFreeJob(context.FirstSelectedPawn, clickedPawn));
            }
            else
            {
                return new FloatMenuOption("Restraints.TryToRestrain".Translate(clickedPawn), () => RunRestrainJob(context.FirstSelectedPawn, clickedPawn));
            }
        }

        public override bool SelectedPawnValid(Pawn pawn, FloatMenuContext context)
        {
            return base.SelectedPawnValid(pawn, context);
        }

        public override bool TargetPawnValid(Pawn pawn, FloatMenuContext context)
        {
            if (!pawn.IsColonist && !pawn.IsPrisonerOfColony && !pawn.IsSlaveOfColony) return false;
            if (pawn.MentalStateDef == MentalStateDefOf.Berserk) return false;
            if (!context.FirstSelectedPawn.CanReach(pawn, PathEndMode.ClosestTouch, Danger.Deadly)) return false;
            return base.TargetPawnValid(pawn, context);
        }

        private static void RunFreeJob(Pawn pawn, Pawn target)
        {
            pawn.jobs.TryTakeOrderedJob(new Job(RestraintsDefs.FreeJob, target));
        }

        private static void RunRestrainJob(Pawn pawn, Pawn target)
        {
            Thing steel = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(ThingDefOf.Steel), PathEndMode.OnCell, TraverseParms.For(pawn));
            if (steel != null)
            {
                pawn.jobs.TryTakeOrderedJob(new Job(RestraintsDefs.RestrainJob, target, steel));
            }
            else
            {
                Messages.Message("Restraints.NeedSteel".Translate(), pawn, MessageTypeDefOf.RejectInput, false);
            }
        }

    }
}
