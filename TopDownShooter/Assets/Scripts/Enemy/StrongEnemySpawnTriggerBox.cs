using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemySpawnerTriggerBox : MonoBehaviour
{
    [Header("Enemy Spawning")]
    [SerializeField] bool addToEnemySpawnQueue = true;
    [SerializeField] int amountOfLevel2RangedEnemiesToAdd = 5;
    [SerializeField] int amountOfLevel2MeleeEnemiesToAdd = 5;
    [Header("Additon Functions")]
    [SerializeField] bool pauseEnemySpawning = false;
    [SerializeField] bool resumeEnemySpawning = false;
    [SerializeField] bool clearEnemySpawnQueue = false;
    bool ran = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ran && collision.gameObject.tag == "Player")
        {
            ran = true;
            if (addToEnemySpawnQueue)
                EnemySpawner.AddToEnemySpawnQueue(amountOfLevel2RangedEnemiesToAdd, amountOfLevel2MeleeEnemiesToAdd);
            if (pauseEnemySpawning)
                EnemySpawner.PauseEnemySpawning();
            if (resumeEnemySpawning)
                EnemySpawner.ResumeEnemySpawning();
            if (clearEnemySpawnQueue)
                EnemySpawner.ClearEnemySpawnQueue();
        }
    }

}
