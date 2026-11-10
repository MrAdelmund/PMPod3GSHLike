using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteAndSmallSpawnerBox : MonoBehaviour
{
    [Header("Enemy Spawning")]
    [SerializeField] bool addToEnemySpawnQueue = true;
    [SerializeField] int amountOfRangedEnemiesToAdd = 5;
    [SerializeField] int amountOfMeleeEnemiesToAdd = 5;
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
                BruteAndSmallSpawner.AddToEnemySpawnQueue(amountOfRangedEnemiesToAdd, amountOfMeleeEnemiesToAdd);
            if (pauseEnemySpawning)
                BruteAndSmallSpawner.PauseEnemySpawning();
            if (resumeEnemySpawning)
                BruteAndSmallSpawner.ResumeEnemySpawning();
            if (clearEnemySpawnQueue)
                BruteAndSmallSpawner.ClearEnemySpawnQueue();
        }
    }

}
