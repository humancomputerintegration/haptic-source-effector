using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : LocomotionStateBehavior
{
    /* Ensures this describes the behavior for the RightDown state */
    private protected override LocomotionState GetState() {
        return LocomotionState.Standing;
    }

    /* Reset the queue and stop moving whenever you enter this state */
    public override void OnActivate() {
        locomotion.Reset();
    }

    /* Change states whenever the user isn't flying and a foot is lifted */
    private protected override LocomotionState FindNextState() {
        if(feet.LeftState != feet.RightState) {
            if (feet.LeftState == FootState.Up) {
                return LocomotionState.LeftUp;
            }
            return LocomotionState.RightUp;
        }
        return LocomotionState.Standing;
    }
}
