using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Make this a Singleton class */
    public static GameManager Instance { get; private set; }
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
    [Header("Game Parameters")]
    [SerializeField, Tooltip("The maximum amount of shots the laser can take")]
    private int maxLaserAmmo;
    [SerializeField, Tooltip("The amount of shots remaining for the laser")]
    private int laserAmmo;
    public int LaserAmmo {
        set {
            laserAmmo = value;
        }
        get {
            return laserAmmo;
        }
    }
    public void ReloadLaser() {
        laserAmmo = maxLaserAmmo;
    }
    [SerializeField, Tooltip("The maximum amount of shots for the projectile shooter")]
    private int maxProjectileAmmo;
    [SerializeField, Tooltip("The amount of shots remaining for the projectile shooter")]
    private int projectileAmmo;
    public int ProjectileAmmo {
        set {
            projectileAmmo = value;
        }
        get {
            return projectileAmmo;
        }
    }
    public void ReloadProjectile() {
        projectileAmmo = maxProjectileAmmo;
    }
    [Header("Enemy Parameters")]
    [SerializeField, Tooltip("The next target the enemies will attack")]
    private PlayerTarget _enemyTarget;
    [SerializeField, Tooltip("The cycle for the enemy attacks")]
    private List<PlayerTarget> enemyTargetCycle;
    [SerializeField, Range(0, 1), Tooltip("The regular enemy shooting probability")]
    private float normalShootProbability;
    public float NormalShootProbability {
        get {
            return normalShootProbability;
        }
    }
    [SerializeField, Range(0, 1), Tooltip("The shooting probability when the player has no ammo left ")]
    private float reducedShootProbability;
    public float ReducedShootProbability {
        get {
            return reducedShootProbability;
        }
    }
    private int targetIdx = -1;
    public PlayerTarget EnemyTarget {
        get {
            PlayerTarget res = _enemyTarget;
            targetIdx = (targetIdx + 1) % enemyTargetCycle.Count;
            _enemyTarget = enemyTargetCycle[targetIdx];
            return res;
        }
    }
    [SerializeField, Tooltip("The maximum number of enemies that can spawn")]
    private int maxEnemies;
    [SerializeField, Tooltip("The number of enemies spawned in so far")]
    private int _curEnemies;
    public int EnemiesRemaining {
        get {
            return maxEnemies - _curEnemies;
        }
        set {
            _curEnemies = maxEnemies - value;
        }
    }

    [Header("Object References")]
    [SerializeField, Tooltip("The Transform of the HMD or of an object tracking the HMD")]
    private Transform _hmd;
    public Transform HMD {
        get {
            return _hmd;
        }
    }
    [SerializeField, Tooltip("The Transform of the right hand or of an object tracking the right hand")]
    private Transform _rightHand;
    public Transform RightHand {
        get {
            return _rightHand;
        }
    }
    [SerializeField, Tooltip("The Transform of the left hand or of an object tracking the left hand")]
    private Transform _leftHand;
    public Transform LeftHand {
        get {
            return _leftHand;
        }
    }
    [SerializeField, Tooltip("The Transform of the right foot or of an object tracking the right foot")]
    private Transform _rightFoot;
    public Transform RightFoot {
        get {
            return _rightFoot;
        }
    }
    [SerializeField, Tooltip("The Transform of the left foot or of an object tracking the left foot")]
    private Transform _leftFoot;
    public Transform LeftFoot {
        get {
            return _leftFoot;
        }
    }
    [SerializeField, Tooltip("The AudioSource of the sound to play when stomping")]
    private AudioSource _stompSound;
    public AudioSource StompSound {
        get {
            return _stompSound;
        }
    }

    private void Start() {
        var temp = EnemyTarget; // The get function needs to call once in order to align values properly
    }

}
