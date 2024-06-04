using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoseTargetVisualizer : MonoBehaviour
{
    [SerializeField, Tooltip("When enabled, this script will show the right hand's grab target instead of the left hand's target")]
    private bool isRight;
    void Update()
    {
        // Give the cylinder the correct dimensions
        transform.localScale = new Vector3(PoseManager.Instance.GrabRadius * 2, PoseManager.Instance.GrabDelta * 0.5f, PoseManager.Instance.GrabRadius * 2);
        
        // Set the hand to the proper reference
        Transform hand = isRight ? GameManager.Instance.RightHand : GameManager.Instance.LeftHand;

        // Move the cylinder behind the hand
        transform.position = hand.position + hand.forward * PoseManager.Instance.GrabDistance - Vector3.up * PoseManager.Instance.GrabVerticalOffset;

        // Orient the cylinder to point in the same direction as the hand
        transform.up = -hand.forward;
    }
}
