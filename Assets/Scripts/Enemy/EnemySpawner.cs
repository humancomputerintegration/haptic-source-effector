using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
struct SpawnedEnemies {
    [SerializeField, ReadOnly, Tooltip("The enemies currently spawned in")]
    private GameObject[] enemies;
    [SerializeField, ReadOnly, Tooltip("The statuses of each enemy")]
    private bool[] enemyStatuses;
    [SerializeField, ReadOnly, Tooltip("The number of enemies still alive")]
    private int _aliveEnemies;
    public int AliveEnemies {
        get {
            return _aliveEnemies;
        }
        private set {
            _aliveEnemies = value;
        }
    }

    public SpawnedEnemies(int numEnemies, float innerRadius, float outerRadius, GameObject enemyPrefab, Transform spawnTarget) {
        enemies = new GameObject[numEnemies];
        enemyStatuses = new bool[numEnemies];
        _aliveEnemies = numEnemies;
        for (int i = 0; i < numEnemies; i++) {
            
            Vector3 spawnPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
            spawnPosition *= Random.Range(innerRadius, outerRadius); 
            spawnPosition += spawnTarget.position;
            enemies[i] = Object.Instantiate(enemyPrefab, spawnPosition, spawnTarget.rotation, spawnTarget);
            enemyStatuses[i] = true;
            GameManager.Instance.EnemiesRemaining -= 1;
            
        }
    }

    public void UpdateStatuses() {
        if (AliveEnemies == 0) {
            return;
        }
        for (int i = 0; i < enemies.Length; i++) {
            if (!enemyStatuses[i]) {
                continue;
            }
            if (!enemies[i].activeSelf) {
                enemyStatuses[i] = false;
                AliveEnemies -= 1;
            }
        }
    }

    public void KillAllEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            Object.Destroy(enemies[i]);
            enemies[i] = null;
        }
    }
}
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning Parameters")]
    [SerializeField, Tooltip("The number of enemies to spawn")]
    private int numEnemies;
    [SerializeField, Tooltip("The inner radius for spawning. Enemies will spawn at least this far away from the target position.")]
    private float innerRadius;
    [SerializeField, Tooltip("The outer radius for spawning. Enemies will spawn at most this far away from the target position.")]
    private float outerRadius;
    [Header("Respawning Parameters")]

    [SerializeField, Tooltip("The delay before respawning enemies")]
    private float spawnDelay;
    [SerializeField, Tooltip("The maximum variance in the spawn delay")]
    private float spawnDelayDelta;
    [SerializeField, ReadOnly, Tooltip("The current time before the enemies respawn")]
    private float spawnTimer = 0;

    [Header("Current Status")]
    [SerializeField, ReadOnly, Tooltip("All information regarding the currently spawned enemies")]
    private SpawnedEnemies spawnedEnemies;
    [SerializeField, Tooltip("When clicked, the enemies will respawn")]
    private bool respawn;
    [Header("Object References")]
    [SerializeField, Tooltip("The target Transform that the enemy will spawn around")]
    private Transform spawnTarget;
    [SerializeField, Tooltip("The enemy prefab")]
    private GameObject enemy;
    
    private void Spawn() {
        if(GameManager.Instance.EnemiesRemaining <= 0) {
            return;
        }
        spawnedEnemies = new SpawnedEnemies(numEnemies, innerRadius, outerRadius, enemy, transform);
    }
    private void Respawn() {
        spawnedEnemies.KillAllEnemies();
        Spawn();
    }
    private void Start() {
        Spawn();
    }
    private void Update() {
        if(respawn) {
            Respawn();
            respawn = false;
            return;
        }
        if (spawnTimer > 0) {
            spawnTimer = Mathf.MoveTowards(spawnTimer, 0, Time.deltaTime);
            if (spawnTimer == 0) {
                Respawn();
            }
            return;
        }
        spawnedEnemies.UpdateStatuses();
        if(spawnedEnemies.AliveEnemies == 0) {
            spawnTimer = spawnDelay + Random.Range(-spawnDelayDelta, spawnDelayDelta);
        }
    }
}
