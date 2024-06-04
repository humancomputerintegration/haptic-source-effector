using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum PlayerTarget {
    None = 0,
    Head,
    LeftHand,
    RightHand,
    LeftFoot,
    RightFoot
}
public class PatrolEnemy : StaticHapticEffect, IHittable
{
    [Header("Patrol Enemy Movement Parameters")]
    [SerializeField, Tooltip("The maximum distance the enemy should be from its starting position")]
    private float maxDistance;
    [SerializeField, Tooltip("The minimum distance the enemy should be from its starting position")]
    private float minDistance;
    [SerializeField, Tooltip("The amount of time before the enemy starts traveling")]
    private float waitTime;
    [SerializeField, Tooltip("The maximum variance in the amount of time before the enemy starts traveling")]
    private float waitDelta;
    [SerializeField, Tooltip("The speed, in meters per second, that the enemy moves when travelling to the next position")]
    private float movementSpeed;
    [SerializeField, Tooltip("When enabled, the enemy will not move on the x-axis")]
    private bool lockX;
    [SerializeField, Tooltip("When enabled, the enemy will not move on the y-axis")]
    private bool lockY;
    [SerializeField, Tooltip("When enabled the enemy will not move on the z-axis")]
    private bool lockZ;
    private Vector3 basePosition;
    [SerializeField, ReadOnly, Tooltip("The position the enemy wants to move to")]
    private Vector3 targetPosition;
    [SerializeField, ReadOnly, Tooltip("The current countdown before the enemy moves")]
    private float moveTimer = 0;
    [Header("Patrol Enemy Shooting Parameters")]
    
    [SerializeField, Tooltip("When disabled, projectiles will no longer fire")]
    private bool canShoot = true;
    [SerializeField, Tooltip("The amount of time between attempted shots")]
    private float shootTime;
    [SerializeField, Tooltip("The maximum variance in the amount of time between shots")]
    private float shootDelta;
    [SerializeField, Tooltip("The amount of time, in seconds, before firing when the projectile should be fired")]
    private float projectileTime;
    [SerializeField, Tooltip("The projectile to fire")]
    private GameObject projectileObj;
    [SerializeField, ReadOnly, Tooltip("The target the enemy is currently aiming at")]
    private PlayerTarget curTarget = PlayerTarget.None;
    private bool queued = false;
    [SerializeField, ReadOnly, Tooltip("The current countdown before the enemy next attempts to shoot")]
    private float shootTimer = 0f;
    [SerializeField, ReadOnly, Tooltip("Whether there is currently a projectile spawned")]
    private bool projectileSpawned = false;
    [SerializeField, Tooltip("When clicked, the enemy will get hit")]
    private bool debugHit;

    /* A callback function that is callened whenever the enemy is hit */
    public void OnHit(Vector3 position) {
        /* Add death effects here! */
        gameObject.SetActive(false);
        Debug.Log("Enemy Hit!");
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    /* A callback function that is called whenever an effect is queued */
    private protected override void OnQueue() {
        /* Start some animations and play some sound effects here */
        queued = true;
        curTarget = GameManager.Instance.EnemyTarget;
        effectType = GetHapticEffectType();
    } 

    /* A callback function that is called whenever this effect is dequeued */
    private protected override void OnDequeue() {
        /* Play a cancellation sound effect here */
        curTarget = PlayerTarget.None;
        queued = false;
    }

    /* A callback function that is called whenever this effect is activated */
    private protected override void OnActivate() {
        /* Add effects that trigger only on shooting (like a projectile firing or a giant laser) here! */
        curTarget = PlayerTarget.None;
        queued = false;
        GetComponent<Collider>().enabled = true;
        projectileSpawned = false;
    }
    
    /* Set references and start the timer */
    private protected override void Start() {
        base.Start();
        ResetShootTimer();
        basePosition = transform.position;
        targetPosition = basePosition;
    }

    /* Resets the shooting timer */
    private void ResetShootTimer() {
        shootTimer = Random.Range(shootTime - shootDelta, shootTime + shootDelta);
    }

    private Transform GetEnemyTarget() {
        switch(curTarget) {
            case PlayerTarget.Head:
                return GameManager.Instance.HMD;
            case PlayerTarget.RightHand:
                return GameManager.Instance.RightHand;
            case PlayerTarget.LeftHand:
                return GameManager.Instance.LeftHand;
            case PlayerTarget.RightFoot:
                return GameManager.Instance.RightFoot;
            case PlayerTarget.LeftFoot:
                return GameManager.Instance.LeftFoot;
        }
        return transform; // This is impossible but required for compilation
    }

    private HapticEffectType GetHapticEffectType() {
        switch(curTarget) {
            case PlayerTarget.Head:
                return HapticEffectType.Jaw;
            case PlayerTarget.RightHand:
                return HapticEffectType.RightHandWeak;
            case PlayerTarget.LeftHand:
                return HapticEffectType.LeftHandWeak;
            case PlayerTarget.RightFoot:
                return HapticEffectType.RightFootWeak;
            case PlayerTarget.LeftFoot:
                return HapticEffectType.LeftFootWeak;
        }
        return HapticEffectType.Jaw; // This is impossible but required for compilation
    }

    /* Manages all logic that regards shooting */
    private void Shoot() {
        GameManager manager = GameManager.Instance;
        float probability = 0;
        if((manager.ProjectileAmmo == 0 && manager.LaserAmmo == 0) || (PoseManager.Instance.LeftShootingPose || PoseManager.Instance.RightShootingPose)) {
            probability = manager.ReducedShootProbability;
        }
        else {
            probability = manager.NormalShootProbability;
        }
        if(!queued && canShoot && shootTimer == 0 && Random.Range(0f, 1f) <= probability && CanQueue()) {
            /* Add code to change TMS values here! */
            Queue();
        }
        else if (queued && !canShoot) {
            Dequeue();
        }

        if (shootTimer == 0 && !queued) {
            ResetShootTimer();
        }
        else {
            shootTimer = Mathf.MoveTowards(shootTimer, 0, Time.deltaTime);
        }

        if (queued && !projectileSpawned && StartUp <= projectileTime) {
            GetComponent<Collider>().enabled = false;
            GameObject projectile = Instantiate(projectileObj, transform.position, transform.rotation, GameManager.Instance.transform);
            projectile.GetComponent<EnemyProjectile>().Setup(projectileTime, GetEnemyTarget());
            projectileSpawned = true;
        }
    }

    /* Updates the position the enemy wants to move to */
    private void SetTargetPosition() {
        targetPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        if(lockX) {
            targetPosition.x = 0;
        }
        if (lockY) {
            targetPosition.y = 0;
        }
        if (lockZ) {
            targetPosition.z = 0;
        }
        targetPosition = basePosition + Random.Range(minDistance, maxDistance) * targetPosition.normalized;

    }

    /* Resets the movement timer */
    private void ResetMoveTimer() {
        moveTimer = Random.Range(waitTime - waitDelta, waitTime + waitDelta);
    }

    /* Moves the enemy to random positions within min and max radius of its starting position */
    private void Move() {
        if (moveTimer > 0) {
            moveTimer = Mathf.MoveTowards(moveTimer, 0, Time.deltaTime);
            if (moveTimer == 0) {
                SetTargetPosition();
            }
        }
        else if(!queued) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            if(transform.position == targetPosition) {
                ResetMoveTimer();
            }
        }
    }

    /* Moves and shoots projectiles whenever possible */
    private void Update() {
        Move();
        Shoot();
        transform.LookAt(GameManager.Instance.HMD);
        if(debugHit) {
            OnHit(transform.position);
            debugHit = false;
        }
    }
}