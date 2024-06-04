using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootPoseDetector
{
    /* Stored references*/
    private PoseManager poseManager;
    private Transform shootHand;
    private Transform grabHand;
    private Transform head;
    public ShootPoseDetector(bool isRightHand){
        poseManager = PoseManager.Instance;
        shootHand = isRightHand ? GameManager.Instance.RightHand : GameManager.Instance.LeftHand;
        grabHand = isRightHand ? GameManager.Instance.LeftHand : GameManager.Instance.RightHand;
        head = GameManager.Instance.HMD;
        
    }

    /* Check if the shoot hand's fingers are withing the proper angular offset of the upward direction */
    [SerializeField, ReadOnly, Tooltip("Whether the shooting hand is detected as pointing upwards")]
    private bool shootHandUp;
    public bool ShootHandUp {
        get {
            shootHandUp = Vector3.Angle(shootHand.up, Vector3.up) <= poseManager.VerticalAngleOffset;
            return shootHandUp;
        }
    }
    /* Check if the shoot hand's palm is within the proper angular offset of the forward direction */
    [SerializeField, ReadOnly, Tooltip("Whether the shooting hand is detected as pointing away")]
    private bool shootHandAway;
    public bool ShootHandAway {
        get {
            shootHandAway = Vector3.Angle(-shootHand.forward, shootHand.position - head.position) <= poseManager.ForwardAngleOffset;
            return shootHandAway;
        }
    }

    /* Check if the grab hand is grabbing the wrist of the shooting hand */
    [SerializeField, ReadOnly, Tooltip("Whether the grabbing hand is detected as behind the shooting hand")]
    private bool grabHandBehind;
    [SerializeField, ReadOnly, Tooltip("The distance the grabbing hand is behind the shooting hand")]
    private float grabHandDistance;
    [SerializeField, ReadOnly, Tooltip("The radial distance of the grabbing hand away from the target position ")]
    private float grabHandRadius;
    public bool GrabHandBehind {
        get {
            Vector3 offsetPosition = grabHand.position - poseManager.GrabVerticalOffset * Vector3.up;
            Vector3 grabOffset = offsetPosition - shootHand.position;
            float backwardDistance = Vector3.Dot(grabOffset, shootHand.forward);
            grabHandDistance = backwardDistance;
            grabHandRadius = (grabOffset - backwardDistance * shootHand.up).magnitude; // Distance in plane perpendicular to back of hand direction is within proper radius
            grabHandBehind = Mathf.Abs(backwardDistance - poseManager.GrabDistance) <= poseManager.GrabDelta && grabHandRadius <= poseManager.GrabRadius;
            return grabHandBehind;
        }
    }

    public bool IsActive {
        get {
            bool temp1 = ShootHandUp;
            bool temp2 = ShootHandAway;
            bool temp3 = GrabHandBehind;
            return ShootHandUp && ShootHandAway && GrabHandBehind;
        }
    }
}
