﻿using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace Restraints
{
    public class JobDriver_RemoveRestraints : JobDriver
    {
        private const TargetIndex PawnInd = TargetIndex.A;

        private Pawn Target => (Pawn)job.GetTarget(PawnInd).Thing;


        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(Target, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {

            yield return Toils_Goto.GotoThing(PawnInd, PathEndMode.InteractionCell)
                .FailOnDespawnedNullOrForbidden(PawnInd)
                .FailOnSomeonePhysicallyInteracting(PawnInd)
                .FailOn(toil => !pawn.CanReach(Target, PathEndMode.InteractionCell, Danger.Deadly));

            yield return Toils_General.Wait(50)
                .FailOnCannotTouch(PawnInd, PathEndMode.InteractionCell)
                .WithProgressBarToilDelay(PawnInd)
                .PlaySustainerOrSound(SoundDefOf.TrapArm);

            yield return new Toil
            {
                initAction = () =>
                {
                    Hediff hediff = Target.health.hediffSet.hediffs.Find(h => h.def == RestraintsDefs.RestraintsHediff);
                    if (hediff != null)
                    {
                        Target.health.RemoveHediff(hediff);
                        Target.needs.mood.thoughts.memories.TryGainMemory(RestraintsDefs.RestrainsMemory);
                    }
                }
            };
        }
    }
}