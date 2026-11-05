using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float horizontalSpawnDist = 12;
    [SerializeField] float verticalSpawnDist = 3;
    [SerializeField] int enemySpawnCap = 10; //stops any more than enemies than the spawn cap from spawning untill one dies/despawns
    [Header("References")]
    [SerializeField] GameObject Level2meleeEnemyPrefab;
    [SerializeField] GameObject Level2rangedEnemyPrefab;
    static int Level2rangedEnemySpawnBacklog = 0;
    static int Level2meleeEnemySpawnBacklog = 0;
    //spawnEnemies bool is for if the spawner is allowed to spawn enemies or not, and is controled by EnemySpawnerBox.
    //It can be used to disable spawning even if the spawn queue has enemies left.
    static bool spawnEnemies = true;
    //doSpawnEnemiesCheck bool stops spawnTimer from being ticked up, this will stop enemies from spawning. 
    //It gets set when no enemies are left in the spawn queue, and gets reset when more are added.
    static bool doSpawnEnemiesCheck = true;
    static int currenlySpawnEnemies = 0;
    float spawnTimer = 0;
    private void Awake()
    {
        //reset static variables
        spawnEnemies = true;
        doSpawnEnemiesCheck = true;
        currenlySpawnEnemies = 0;
        Level2rangedEnemySpawnBacklog = 0;
        Level2meleeEnemySpawnBacklog = 0;
    }
    private void Update()
    {
        if (doSpawnEnemiesCheck)
            spawnTimer += Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (spawnEnemies && spawnTimer >= spawnInterval && currenlySpawnEnemies < enemySpawnCap)
            SpawnEnemies();
    }
    void SpawnEnemies()
    {
        //reset timer
        spawnTimer = 0;
        //stops enemy spawning if queue is empty
        if (Level2meleeEnemySpawnBacklog <= 0 && Level2rangedEnemySpawnBacklog <= 0)
            doSpawnEnemiesCheck = false;
        else
        {
            //gets a random enemy to spawn, gives more weight to the larger queue.
            int randomEnemyToSpawn = Random.Range(0, Level2meleeEnemySpawnBacklog + Level2rangedEnemySpawnBacklog);
            if (randomEnemyToSpawn < Level2rangedEnemySpawnBacklog)
            {
                Instantiate(Level2rangedEnemyPrefab, GetSpawnLocation(), Quaternion.Euler(Vector3.zero));
                Level2rangedEnemySpawnBacklog--;
            }
            else
            {
                Instantiate(Level2meleeEnemyPrefab, GetSpawnLocation(), Quaternion.Euler(Vector3.zero));
                Level2meleeEnemySpawnBacklog--;
            }
            currenlySpawnEnemies++;
        }
    }
    Vector2 GetSpawnLocation()
    {
        Vector2 spawnPos = transform.position;
        bool spawnSide = (Random.Range(0, 2) == 1) ? true : false; //generates a random bool value for the direction the enemy should be spawned in
        spawnPos = new Vector2(spawnPos.x + (horizontalSpawnDist * ((spawnSide) ? -1 : 1)), spawnPos.y + verticalSpawnDist);
        return spawnPos;
    }
    //the names are very self explanatory
    public static void AddToEnemySpawnQueue(int amountOfLevel2RangedEnemies, int amountOfLevel2MeleeEnemies)
    {
        Level2rangedEnemySpawnBacklog += amountOfLevel2RangedEnemies;
        Level2meleeEnemySpawnBacklog += amountOfLevel2MeleeEnemies;
        doSpawnEnemiesCheck = true;
    }
    public static void PauseEnemySpawning()
    {
        spawnEnemies = false;
    }
    public static void ResumeEnemySpawning()
    {
        spawnEnemies = true;
    }
    public static void ClearEnemySpawnQueue()
    {
       Level2rangedEnemySpawnBacklog = 0;
        Level2meleeEnemySpawnBacklog = 0;
    }
    //this gets called when an enemy dies/despawns
    public static void SubtractFromSpawnedEnemies()
    {
        currenlySpawnEnemies--;
    }
    //shows the positons of the enemy spawn points in the editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(horizontalSpawnDist, verticalSpawnDist), Vector2.one);
        Gizmos.DrawWireCube(new Vector2(-horizontalSpawnDist, verticalSpawnDist), Vector2.one);
    }
}


