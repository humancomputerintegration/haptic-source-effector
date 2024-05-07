using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FootState {
    Down,
    Up
}

public class Foot : MonoBehaviour
{
    [SerializeField, Tooltip("When enabled, this object will track the behavior of the right foot instead of the left")]
    private bool isRight;
    
    private float floorY = 0;
    private FootState curState;
    public FootState State {
        get { 
            return curState; 
        }
    }
    public void SetFloorPosition() {
        Transform foot = isRight ? GameManager.Instance.RightFoot : GameManager.Instance.LeftFoot;
        floorY = foot.position.y;
    }

    /* 
     * Update the current state when you are close to a different state boundary 
     * - The boundary for the Down state is floorY
     * - The boundary for the Up state is floorY + LiftHeight
     */
    void Update()
    {
        Transform foot = isRight ? GameManager.Instance.RightFoot : GameManager.Instance.LeftFoot;
        if(curState == FootState.Down && foot.position.y >= floorY + FeetManager.Instance.LiftHeight) {
            curState = FootState.Up;
        }
        else if(curState == FootState.Up && foot.position.y <= floorY + FeetManager.Instance.Leniency) {
            curState = FootState.Down;
        }
    }
}
