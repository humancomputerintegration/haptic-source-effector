using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightUpState : LocomotionStateBehavior
{
    /* Ensures this describes the behavior for the RightUp state */
    private protected override LocomotionState GetState() {
        return LocomotionState.RightUp;
    }

    /* Change states when the right foot goes down or if the person is somehow flying */
    private protected override LocomotionState FindNextState() {
        if (feet.LeftState == feet.RightState && feet.LeftState == FootState.Up) {
            return LocomotionState.Standing;
        }
        else if (feet.LeftState == feet.RightState) {
            return LocomotionState.RightDown;
        }
        return LocomotionState.RightUp;
    }
}
