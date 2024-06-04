using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Button : StaticHapticEffect
{
    [Header("Button Properties")]
    [SerializeField, Tooltip("When enabled, this script will detect pressses from the right hand instead of the left hand")]
    private protected bool isRight;
    [SerializeField, Tooltip("When enabled, buttons will constantly attempt to queue the haptic effect manager to try to enable stomp detection whenever possible")]
    private bool forceDetection;
    [SerializeField, ReadOnly, Tooltip("Whether or not the tracked button is currently above the collider")]
    private bool aboveCollider;
    private new Collider collider;
    [SerializeField, ReadOnly, Tooltip("The closest point on the collider to the hand")]
    private Vector3 closestPoint;
    [SerializeField, ReadOnly, Tooltip("Whether or not this is currently checking for presses")]
    private bool _detectingPresses;
    private bool DetectingPresses {
        get {
            return _detectingPresses;
        }
        set {
            if (value == false) {
                Dequeue();
            }
            _detectingPresses = value;
        }
    }
    [SerializeField, Tooltip("When clicked, the script will simulate a user pressing the button")]
    private bool simulatePress;

    /* Allows the box to detect stomps */
    public void StartStompDetection() {
        DetectingPresses = true;
    }

    /* Stops the box from detecting stomps */
    public void StopStompDetection() {
        DetectingPresses = false;
    }

    private protected override void Start() {
        base.Start();
        collider = GetComponent<Collider>();
        autoActivate = false;
    }

    /* A callback function that is called whenever an effect is queued */
    private protected override void OnQueue() {
        
    } 

    /* A callback function that is called whenever this effect is dequeued */
    private protected override void OnDequeue() {

    }

    /* A callback function that is called whenever this effect is activated */
    private protected override void OnActivate() {
        OnPress();
    }

    /* Read up and down movements to detect stomps */
    private void Update() {
        if(simulatePress) {
            simulatePress = false;
            Activate();
        }
        if(forceDetection) {
            DetectingPresses = true;
        }
        if(!DetectingPresses) {
            aboveCollider = false;
            return;
        }
        if(DetectingPresses && !IsQueued()) {
            Queue();
            return;
        }
        Transform hand = isRight ? GameManager.Instance.RightHand : GameManager.Instance.LeftHand;
        closestPoint = collider.ClosestPoint(hand.position);
        bool curInCollider = false;
        bool curAboveCollider = false;
        if(Vector3.Distance(hand.position, closestPoint) < Mathf.Epsilon) {
            curInCollider = true;
        }
        else if (hand.position.y > closestPoint.y) {
            curAboveCollider = true;
        }
        if(curInCollider && aboveCollider) {
            Activate();
            
        }
        aboveCollider = curAboveCollider;
    }

    /* A callback function that is called whenever a stomp is detected */
    private protected virtual void OnPress() {

    }
}
