using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : MonoBehaviour
{
    /* Make this a Singleton class */
    public static PoseManager Instance { get; private set; }
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
    [Header("Calibration Parameters")]
    [SerializeField, Tooltip("The angular offset, in degrees, for vertical detection of the shooting hand")]
    private float _verticalAngleOffset;
    public float VerticalAngleOffset {
        get {
            return _verticalAngleOffset;
        }
    }
    [SerializeField, Tooltip("The angular offset, in degrees, for forward detection of the shooting hand")]
    private float _forwardAngleOffset;
    public float ForwardAngleOffset {
        get {
            return _forwardAngleOffset;
        }
    }
    [SerializeField, Tooltip("The distance, in meters, that the grabbing hand should be behind the shooting hand")]
    private float _grabDistance;
    public float GrabDistance {
        get {
            return _grabDistance;
        }
    }
    [SerializeField, Tooltip("The furthest distance, in meters, in the shooting direction away from the target position that the grabbing hand can be")]
    private float _grabDelta;
    public float GrabDelta {
        get {
            return _grabDelta;
        }
    }
    [SerializeField, Tooltip("The height, in meters, the grab target should be below the shooting hand")]
    private float _grabVerticalOffset;
    public float GrabVerticalOffset {
        get {
            return _grabVerticalOffset;
        }
    }
    [SerializeField, Tooltip("The maximum distance, in meters, in directions perpendicular to the shooting direction where the grabbing hand can be away from its target position")]
    private float _grabRadius;
    public float GrabRadius {
        get {
            return _grabRadius;
        }
    }
    [Header("Current Statuses")]
    [SerializeField, ReadOnly, Tooltip("Whether the left shooting pose is detected")]
    private bool _leftShootingPose = false;
    public bool LeftShootingPose {
        get {
            _leftShootingPose = leftDetector.IsActive;
            return _leftShootingPose;
        }
    }
    [SerializeField, ReadOnly, Tooltip("Whether the right shooting pose is detected")]
    private bool _rightShootingPose = false;
    public bool RightShootingPose {
        get {
            _rightShootingPose = rightDetector.IsActive;
            return _rightShootingPose;
        }
    }
    [SerializeField, ReadOnly, Tooltip("The detector for the left hand shooting pose")]
    private ShootPoseDetector leftDetector;
    [SerializeField, ReadOnly, Tooltip("The detector for the right hand shooting pose")]
    private ShootPoseDetector rightDetector;
    [Header("Audio Parameters")]
    [SerializeField, Tooltip("The AudioSource to play when the pose is detected")]
    private AudioSource chargeSound;
    [SerializeField, ReadOnly, Tooltip("Whether or not audio is currently playing")]
    private bool audioPlaying = false;
    private void Start() {
        leftDetector = new ShootPoseDetector(false);
        rightDetector = new ShootPoseDetector(true);
    }
    private void Update() {
        bool right = RightShootingPose;
        bool left = LeftShootingPose;
        if(GameManager.Instance.LaserAmmo > 0) {
            if(right && !audioPlaying) {
                audioPlaying = true;
                chargeSound.Play();
            }
            else if (!right && audioPlaying) {
                audioPlaying = false;
                chargeSound.Stop();
            }
        }
        else if (audioPlaying) {
            chargeSound.Stop();
        }
        
    }
}
