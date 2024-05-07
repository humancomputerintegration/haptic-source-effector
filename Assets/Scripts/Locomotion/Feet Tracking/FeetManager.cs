using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetManager : MonoBehaviour
{
    /* Make this a Singleton class */
    public static FeetManager Instance { get; private set; }
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

    [SerializeField, Tooltip("Press this button to calibrate both feet")]
    private bool calibrate;
    [SerializeField, Tooltip("The height above the ground where a foot is considered up")]
    private float _liftHeight;
    public float LiftHeight {
        get { return _liftHeight; }
    }
    [SerializeField, Tooltip("The distance away from the target positions of the foot where the foot is considered close enough and allowed to change states")]
    private float _leniency;
    public float Leniency {
        get { return _leniency; }
    }
    [SerializeField, Tooltip("The left foot")]
    private Foot _left;
    public Foot Left {
        get {
            return _left;
        }
    }
    [SerializeField, Tooltip("The right foot")]
    private Foot _right;
    public Foot Right {
        get {
            return _right;
        }
    }
    /* Public access to the states of both feet */
    public FootState LeftState {
        get { return Left.State; }
    }
    public FootState RightState {
        get { return Right.State; }
    }

    [SerializeField, ReadOnly, Tooltip("Whether the left foot is currently up")]
    private bool leftUp;
    [SerializeField, ReadOnly, Tooltip("Whether the right foot is currently up")]
    private bool rightUp;
    private void Update() {
        if(calibrate) {
            calibrate = false;
            Left.SetFloorPosition();
            Right.SetFloorPosition();
        }
        leftUp = LeftState == FootState.Up;
        rightUp = RightState == FootState.Up;
    }

}
