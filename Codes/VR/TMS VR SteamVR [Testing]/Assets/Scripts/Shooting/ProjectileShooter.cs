using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : DynamicHapticEffect
{
    [Header("Projectile Shooting Parameters")]
    [SerializeField, Tooltip("The speed of the projectile when it is launched")]
    private float launchSpeed;
    [SerializeField, Tooltip("The vertical angle, in degrees, to offset the launch")]
    private float angleOffset;
    [SerializeField, Tooltip("When disabled, projectiles will no longer fire")]
    private bool canShoot = true;
    [SerializeField, Tooltip("When enabled, this belongs to the right hand. When disabled, this belongs to the left hand.")]
    private bool rightHand = false;
    [SerializeField, Tooltip("The projectile to launch")]
    private GameObject projectile;
    public Transform Hand {
        get {
            return rightHand ? GameManager.Instance.RightHand : GameManager.Instance.LeftHand;
        }
    }

    private bool queued = false;
    /* A callback function that is called whenever an effect is queued */
    private protected override void OnQueue() {
        /* Start some animations and play some sound effects here */
        queued = true;
    } 

    /* A callback function that is called whenever this effect is dequeued */
    private protected override void OnDequeue() {
        /* Play a cancellation sound effect here */
        queued = false;
    }

    /* A callback function that is called whenever this effect is activated */
    private protected override void OnActivate() {
        GameManager.Instance.ProjectileAmmo -= 1;
        GameObject newProjectile = GameObject.Instantiate(projectile, Hand.position, Hand.rotation);
        Vector3 forward = -Hand.forward;
        Vector3 launchDirection = Vector3.RotateTowards(forward, Vector3.up, angleOffset * Mathf.Deg2Rad, 0);
        newProjectile.GetComponent<Rigidbody>().AddForce(
            launchSpeed * launchDirection, ForceMode.VelocityChange);
        queued = false;
    }
    
    
    /* Queue shooting projectiles whenever it is possible */
    private void Update() {
        transform.position = Hand.position;
        bool poseActive = rightHand ? PoseManager.Instance.RightShootingPose : PoseManager.Instance.LeftShootingPose;
        if(!queued && canShoot && poseActive && GameManager.Instance.ProjectileAmmo > 0 && CanQueue()) {
            Queue();
        }
        else if (queued && !(canShoot && poseActive)) {
            Dequeue();
        }
    }
}
