using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocomotionState {
    Standing,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown
}
// This script will not work if you do not have behaviors for each type of Locomotion State
[RequireComponent(typeof(StandingState)), RequireComponent(typeof(LeftUpState)), RequireComponent(typeof(LeftDownState)), RequireComponent(typeof(RightUpState)), RequireComponent(typeof(RightDownState))]
public class Locomotion : MonoBehaviour
{
    /* Make this a Singleton class */
    public static Locomotion Instance { get; private set; }
    private void Awake() 
    {   
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    [SerializeField, Tooltip("The object to move")]
    private Transform moveTarget;
    [SerializeField, Tooltip("The distance the user travels each step")]
    private float distancePerStep;
    [SerializeField, Tooltip("The acceleration, in units per second^2, of movement")]
    private float acceleration;
    [SerializeField, Tooltip("The acceleration when stopping movement")]
    private float deceleration;
    [SerializeField, Tooltip("The minimum number of steps before movement starts")]
    private int minSteps;
    [SerializeField, Tooltip("The maximum number of steps to consider when finding the average speed")]
    private int maxSteps;
    [SerializeField, Tooltip("The maximum time, in seconds, between steps before returning to standing")]
    private float maxStepTime;
    public float MaxStepTime {
        get { return maxStepTime; }
    }
    
    // Stores all of the state bahaviors
    private Dictionary<LocomotionState, LocomotionStateBehavior> stateBehaviors = new Dictionary<LocomotionState, LocomotionStateBehavior>();
    // Stores all of the most recent periods (time between a foot going up and going down)
    private List<float> periods = new List<float>();
    // A constantly counting timer used for period calculations
    private float periodTimer;
    public float Timer {
        get { return periodTimer; }
    }
    // The first period after a reset doesn't count
    private bool justReset = false;
    // The current speed 
    [SerializeField, ReadOnly, Tooltip("The current speed of the user")]
    private float curSpeed;
    [SerializeField, ReadOnly, Tooltip("The current locomotion state")]
    private LocomotionState curState = LocomotionState.Standing;
    [SerializeField, Tooltip("Hit this button to reset the position and speed of the user")]
    private bool reset;
    [SerializeField, Tooltip("When toggled, locomotion will not occur")]
    private bool disabled;
    
    /* Find all state behaviors attached to this object */
    private void Start()
    {
        foreach (LocomotionStateBehavior stateBehavior in transform.GetComponents<LocomotionStateBehavior>()) {
            stateBehaviors.Add(stateBehavior.State, stateBehavior);
        }
        if(stateBehaviors.Count != Enum.GetNames(typeof(LocomotionState)).Length) {
            Debug.Log("Error: Some locomotion states are not defined! Disabling locomotion...");
            gameObject.SetActive(false);
        }
    }

    /* Add the current period to the queue and adjust the queue's length if possible */
    public void QueuePeriod() {
        if(justReset) {
            justReset = false;
            return;
        }
        periods.Add(periodTimer);
        periodTimer = 0; 
        if(periods.Count > maxSteps) {
            periods.RemoveAt(0);
        }
    }

    /* Removes all stored periods to stop locomotion */
    public void Reset() {
        justReset = true;
        periods.Clear();
        periodTimer = 0;
    }

    /* Activates the next state */
    public void ChangeState(LocomotionState newState) {
        curState = newState;
        stateBehaviors[curState].OnActivate();
    }

    /* Calculates the speed by using the periods to find the average frequency*/
    private float CalculateSpeed() {
        if(periods.Count <= minSteps) {
            return 0;
        }
        float averageFrequency = 0;
        foreach (float period in periods) {
            averageFrequency += 1 / period; // Frequency = 1 / Period
        }
        averageFrequency /= periods.Count;
        return averageFrequency * distancePerStep;
    }

    /* Updates the timer, each state, and the current speed */
    private void Update() {
        if (disabled) {
            return;
        }
        periodTimer += Time.deltaTime;
        stateBehaviors[curState].UpdateState();

        // Move the user
        float nextSpeed = CalculateSpeed();
        curSpeed = Mathf.MoveTowards(curSpeed, nextSpeed, (nextSpeed == 0 ? deceleration : acceleration) * Time.deltaTime);
        Vector3 direction = GameManager.Instance.HMD.transform.forward;
        direction.y = 0;
        moveTarget.position += curSpeed * direction;

        if(reset) {
            reset = false;
            moveTarget.position = Vector3.zero;
            Reset();
            curSpeed = 0;
        }
    }
}
