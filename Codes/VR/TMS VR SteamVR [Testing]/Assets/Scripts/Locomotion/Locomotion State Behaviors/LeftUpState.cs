using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUpState : LocomotionStateBehavior
{
    /* Ensures this describes the behavior for the LeftUp state */
    private protected override LocomotionState GetState() {
        return LocomotionState.LeftUp;
    }

    /* Change states when the left foot goes down or if the person is somehow flying */
    private protected override LocomotionState FindNextState() {
        if (feet.LeftState == feet.RightState && feet.LeftState == FootState.Up) {
            return LocomotionState.Standing;
        }
        else if (feet.LeftState == feet.RightState) {
            return LocomotionState.LeftDown;
        }
        return LocomotionState.LeftUp;
    }
}
