using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocomotionStateBehavior : MonoBehaviour
{
    // Stores the state this script describes
    private LocomotionState state;
    public LocomotionState State {
        get { return state; }
    }

    // References the Locomotion instance for more readable code
    private protected Locomotion locomotion;
    // References the FeetManager instance for more readable code
    private protected FeetManager feet;

    /* Set the current state */
    private void Awake() {
        state = GetState();
    }
    
    /* Sets references and the current state */
    private void Start() {
        locomotion = Locomotion.Instance;
        feet = FeetManager.Instance;
    }

    /* Changes states if necessary */
    public void UpdateState() {
        LocomotionState nextState = FindNextState();
        if(State != nextState) {
            Locomotion.Instance.ChangeState(nextState);
        }
    }

    /* Use this function to set the state of the entire script */
    private protected virtual LocomotionState GetState() {
        return LocomotionState.Standing;
    }

    /* A callback function that is called whenever this state activates */
    public virtual void OnActivate() {
        return;
    }

    /* 
     * Determines which state the program should move to in the next frame 
     * - Return the current state if it should not change states 
     */
    private protected virtual LocomotionState FindNextState() {
        return LocomotionState.Standing;
    }
}
