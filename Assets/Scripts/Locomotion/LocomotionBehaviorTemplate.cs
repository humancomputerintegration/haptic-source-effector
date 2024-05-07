using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionStateBehaviorTemplate : LocomotionStateBehavior
{
    /* Use this function to set the state of the entire script */
    private protected override LocomotionState GetState() {
        return LocomotionState.Standing;
    }

    /* A callback function that is called whenever this state activates */
    public override void OnActivate() {
        return;
    }

    /* 
     * Determines which state the program should move to in the next frame 
     * - Return the current state if it should not change states 
     */
    private protected override LocomotionState FindNextState() {
        return LocomotionState.Standing;
    }
}
