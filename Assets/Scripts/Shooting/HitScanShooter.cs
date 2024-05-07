using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
public class HitScanShooter : StaticHapticEffect
{
    [Header("HitScan Shooting Parameters")]
    [SerializeField, Tooltip("The radius of the laser")]
    private float radius;
    [SerializeField, Tooltip("The maximum distance of the Raycast")]
    private float maxDistance;
    [SerializeField, Tooltip("When disabled, projectiles will no longer fire")]
    private bool canShoot = true;
    [SerializeField, Tooltip("When enabled, this belongs to the right hand. When disabled, this belongs to the left hand.")]
    private bool rightHand = false;
    [SerializeField, Tooltip("Enable this to print debug logs whenever a laser is shot")]
    private bool debug;
    public Transform Hand {
        get {
            return rightHand ? GameManager.Instance.RightHand : GameManager.Instance.LeftHand;
        }
    }

    [SerializeField] ShootSpawnPoint shootSpawnPoint;

    private bool queued = false;
    /* A callback function that is called whenever an effect is queued */
    private protected override void OnQueue() {
        OnStartCharge();
        queued = true;
    } 

    private protected virtual void OnStartCharge() {
        return;
    }

    /* A callback function that is called whenever this effect is dequeued */
    private protected override void OnDequeue() {
        OnCancelCharge();
        queued = false;
    }

    /* Play a cancellation sound effect here */
    private protected virtual void OnCancelCharge() {
        return;
    }

    /* A callback function that is called whenever this effect is activated */
    private protected override void OnActivate() {
        shootSpawnPoint.DoShoot();
        GameManager.Instance.LaserAmmo -= 1;
        // Vector3 forward = -Hand.forward;
        // Debug.DrawRay(Hand.position, forward, Color.blue, 5);
        // if (Physics.SphereCast(
        //         Hand.position, 
        //         radius,
        //         forward,
        //         out RaycastHit hit,
        //         maxDistance,
        //         1 << LayerMask.NameToLayer("Hittable"),
        //         QueryTriggerInteraction.Collide)) {
        //     if(hit.collider.TryGetComponent<IHittable>(out IHittable hittable)) {
        //         hittable.OnHit(hit.point);
        //         OnShoot();
        //     }
        //     Debug.Log("HIT");
        // }
        // else {
        //     Debug.Log("MISS :(");
        // }

        Vector3 forward = -Hand.forward;
        var hittables = FindObjectsOfType<MonoBehaviour>().OfType<IHittable>();
        foreach(IHittable target in hittables) {
            Vector3 shooterToObj = target.GetPosition() - Hand.position;
            float forwardDistance = Vector3.Dot(shooterToObj, forward);
            if(forwardDistance < 0) {
                continue;
            }
            Vector3 projection = shooterToObj - forwardDistance * forward;
            if(projection.magnitude <= radius) {
                target.OnHit(target.GetPosition());
                Debug.Log("Hit!");
            }
        }

        queued = false;
    }

    private protected virtual void OnShoot() {
        return;
    }
    
    
    /* Queue shooting projectiles whenever it is possible */
    private void Update() {
        transform.position = Hand.position;
        bool poseActive = rightHand ? PoseManager.Instance.RightShootingPose : PoseManager.Instance.LeftShootingPose;
        if(!queued && canShoot && poseActive && GameManager.Instance.LaserAmmo > 0 && CanQueue()) {
            Queue();
        }
        else if (queued && !(canShoot && poseActive)) {
            Dequeue();
        }
    }
}