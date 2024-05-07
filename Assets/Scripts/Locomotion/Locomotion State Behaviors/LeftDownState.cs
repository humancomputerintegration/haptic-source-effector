using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDownState : LocomotionStateBehavior
{
    /* Ensures this describes the behavior for the RightDown state */
    private protected override LocomotionState GetState() {
        return LocomotionState.LeftDown;
    }

    /* Add the current period to the queue whenever you enter this state */
    public override void OnActivate() {
        locomotion.QueuePeriod();
    }

    /* Change states if nothing has happend for too long, if the user is somehow flying, or if the right foot is lifted */
    private protected override LocomotionState FindNextState() {
        if(locomotion.Timer > locomotion.MaxStepTime) {
            return LocomotionState.Standing;
        }
        else if (feet.RightState == FootState.Up){
            return LocomotionState.RightUp;
        }
        return LocomotionState.LeftDown;
    }
}
