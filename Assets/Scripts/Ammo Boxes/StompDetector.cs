using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StompDetector : StaticHapticEffect
{
    [Header("Stomp Detector Properties")]
    [SerializeField, Tooltip("When enabled, this script will detect stomps from the right foot instead of the left foot")]
    private protected bool isRight;
    [SerializeField, Tooltip("When enabled, stomps will constantly attempt to queue the haptic effect manager to try to enable stomp detection whenever possible")]
    private bool forceDetection;
    [SerializeField, ReadOnly, Tooltip("Whether or not the tracked foot is currently above the collider")]
    private bool aboveCollider;
    private new Collider collider;
    [SerializeField, ReadOnly, Tooltip("The closest point on the collider to the foot")]
    private Vector3 closestPoint;
    [SerializeField, ReadOnly, Tooltip("Whether or not this is currently checking for stomps")]
    private bool _detectingStomps;
    private bool DetectingStomps {
        get {
            return _detectingStomps;
        }
        set {
            if (value == false) {
                Dequeue();
            }
            _detectingStomps = value;
        }
    }
    [SerializeField, Tooltip("When clicked, the script will simulate a user stomping")]
    private bool simulateStomp;

    /* Allows the box to detect stomps */
    public void StartStompDetection() {
        DetectingStomps = true;
    }

    /* Stops the box from detecting stomps */
    public void StopStompDetection() {
        DetectingStomps = false;
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
        OnStomp();
    }

    /* Read up and down movements to detect stomps */
    private void Update() {
        if(simulateStomp) {
            simulateStomp = false;
            Activate();
        }
        if(forceDetection) {
            DetectingStomps = true;
        }
        if(!DetectingStomps) {
            aboveCollider = false;
            return;
        }
        if(DetectingStomps && !IsQueued()) {
            Queue();
            return;
        }
        Transform foot = isRight ? GameManager.Instance.RightFoot : GameManager.Instance.LeftFoot;
        closestPoint = collider.ClosestPoint(foot.position);
        bool curInCollider = false;
        bool curAboveCollider = false;
        if(Vector3.Distance(foot.position, closestPoint) < Mathf.Epsilon) {
            curInCollider = true;
        }
        else if (foot.position.y > closestPoint.y) {
            curAboveCollider = true;
        }
        if(curInCollider && aboveCollider) {
            Activate();
            
        }
        aboveCollider = curAboveCollider;
    }

    /* A callback function that is called whenever a stomp is detected */
    private protected virtual void OnStomp() {

    }
}
