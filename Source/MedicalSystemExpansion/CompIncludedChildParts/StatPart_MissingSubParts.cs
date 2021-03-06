﻿using RimWorld;
using UnityEngine;
using Verse;

namespace MSE2
{
    internal class StatPart_MissingSubParts : StatPart
    {
        // This stat part impacts market value subtracting the value of missing subparts

        public override string ExplanationPart ( StatRequest req )
        {
            if ( req.HasThing )
            {
                var comp = req.Thing.TryGetComp<CompIncludedChildParts>();

                if ( comp != null && comp.MissingValue != 0f )
                {
                    return "Some parts are missing: -$" + comp.MissingValue;
                }
            }

            return null;
        }

        public override void TransformValue ( StatRequest req, ref float val )
        {
            if ( req.HasThing )
            {
                var comp = req.Thing.TryGetComp<CompIncludedChildParts>();

                if ( comp != null )
                {
                    // can't go below 20%
                    val = Mathf.Max( val * 0.2f, val - comp.MissingValue );
                }
            }
        }
    }
}