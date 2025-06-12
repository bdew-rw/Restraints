using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Restraints
{
    public class RestraintsMod : Mod
    {
        public readonly RestraintsSettings settings;

        public RestraintsMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<RestraintsSettings>();
            Log.Message("Restraints loaded");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            AddSettingsNumberLine(listing, "RestraintBerserkChance", ref RestraintsSettings.RestraintBerserkChance, 0, 1);

            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        private static void AddSettingsNumberLine(Listing_Standard listing, string name, ref float val, float min, float max)
        {
            var rect = listing.GetRect(30f);

            var rectLabel = rect.LeftPart(0.5f).Rounded();
            var rectRight = rect.RightPart(0.5f).Rounded();
            var rectField = rectRight.LeftPart(0.25f).Rounded();
            var rectSlider = rectRight.RightPart(0.7f).Rounded();

            Text.Anchor = TextAnchor.MiddleLeft;

            TooltipHandler.TipRegion(rect, $"Restraints.{name}.Desc".Translate());

            Widgets.Label(rectLabel, $"Restraints.{name}".Translate());

            string buffer = $"{val:F2}";
            Widgets.TextFieldNumeric<float>(rectField, ref val, ref buffer, min, max);

            Text.Anchor = TextAnchor.UpperLeft;

            float num = Widgets.HorizontalSlider(rectSlider, val, min, max, middleAlignment: true);

            if (num != val)
            {
                SoundDefOf.DragSlider.PlayOneShotOnCamera();
                val = num;
            }

            listing.Gap(2f);
        }


        public override string SettingsCategory()
        {
            return "Restraints.Name".Translate();
        }
    }
}