using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Left = 0,
    Right = 1
}
public class AmmoManager : MonoBehaviour
{
    public static AmmoManager Instance { get; private set; }
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
    [Header("Regular Spawning Parameters")]
    [SerializeField, Range(0, 1), Tooltip("The probability, in the interval [0, 1], that the box will spawn each time a spawn is attempted")]
    private float spawnProbility;
    [SerializeField, Tooltip("The amount of time, in seconds, between spawn attempts")]
    private float spawnTime;
    [SerializeField, Tooltip("The location the next ammo box will spawn in")]
    private Direction nextSpawnLocation;
    [Header("Object References")]
    [SerializeField, Tooltip("The Transform that is at the left spawn location")]
    private Transform leftSpawn;
    [SerializeField, Tooltip("The Transform that is at the left target location")]
    private Transform leftTarget;
    [SerializeField, Tooltip("The Transform that is at the right spawn location")]
    private Transform rightSpawn;
    [SerializeField, Tooltip("The Transform that is at the right target location")]
    private Transform rightTarget;
    [SerializeField, Tooltip("The Transform that is at the button's target location")]
    private Transform buttonTarget;
    [SerializeField, Tooltip("The Ammo Box gameobject prefab to spawn in for the left ammo box")]
    private GameObject leftAmmoBox;
    [SerializeField, Tooltip("The Ammo Box gameobject prefab to spawn in for the right ammo box")]
    private GameObject rightAmmoBox;
    [SerializeField, Tooltip("The button to spawn when the remaining enemies is 0")]
    private GameObject escapeButton;
    [Header("Forced Spawning Parameters")]
    [SerializeField, Tooltip("The location the forced ammo box will spawn in")]
    private Direction forcedSpawnLocation;
    [SerializeField, Tooltip("When clicked, a forced spawn will initiate")]
    private bool forceSpawn;
    [SerializeField, ReadOnly, Tooltip("The current ammo box that is spawned in")]
    private GameObject curAmmoBox = null;

    float spawnTimer = 0;
    private void Spawn(Direction location) {
        if(curAmmoBox != null) {
            return;
        }
        if(GameManager.Instance.EnemiesRemaining <= 0) {
            curAmmoBox = Instantiate(escapeButton, buttonTarget.position, buttonTarget.rotation, transform);
            return;
        }
        Transform spawn;
        Transform target;
        GameObject ammoBox;
        switch (location) {
            case Direction.Left:
                spawn = leftSpawn;
                target = leftTarget;
                ammoBox = leftAmmoBox;
                break;
            case Direction.Right:
                spawn = rightSpawn;
                target = rightTarget;
                ammoBox = rightAmmoBox;
                break;
            default: /* Will never get called but needed because the C# compiler is stupid */
                spawn = transform;
                target = transform;
                ammoBox = leftAmmoBox;
                break;
        }
        curAmmoBox = Instantiate(ammoBox, spawn.position, spawn.rotation, transform);
        curAmmoBox.GetComponent<MoveStompToTarget>().Setup(target);
    }

    private void SetNextSpawnLocation() {
        nextSpawnLocation = (Direction) (((int) nextSpawnLocation + 1) % System.Enum.GetNames(typeof(Direction)).Length);
    }
    private void Update() {
        if(curAmmoBox != null) {
            spawnTimer = 0;
            return;
        }
        if(forceSpawn) {
            Spawn(forcedSpawnLocation);
            forceSpawn = false;
        }
        if(GameManager.Instance.ProjectileAmmo != 0 || GameManager.Instance.LaserAmmo != 0) {
            return;
        }
        if(spawnTimer == 0) {
            if(Random.Range(0f, 1f) <= spawnProbility) {
                Spawn(nextSpawnLocation);
                SetNextSpawnLocation();
            }
            else {
                spawnTimer = spawnTime;
            }
        }
        else {
            spawnTimer = Mathf.MoveTowards(spawnTimer, 0, Time.deltaTime);
        }
    }

}
