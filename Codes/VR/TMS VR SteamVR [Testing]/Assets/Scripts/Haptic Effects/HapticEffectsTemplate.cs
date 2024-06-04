using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Actual script templates are not project specific for some reason so copy-pasting is the best solution */
public class HapticEffectTemplate : HapticEffect
{
    /* Set any references you may need here */
    private protected override void Start() {
        base.Start();
    }

    /* Determines the current cooldown */
    private protected override float GetCooldown() {
        return 0;
    }

    /* Determines whether the current cooldown is valid */
    private protected override bool ValidateCooldown() {
        return true;
    }

    /* A callback function that is called whenever an effect is queued */
    private protected override void OnQueue() {

    } 

    /* A callback function that is called whenever this effect is dequeued */
    private protected override void OnDequeue() {

    }

    /* A callback function that is called whenever this effect is activated */
    private protected override void OnActivate() {

    }
}
