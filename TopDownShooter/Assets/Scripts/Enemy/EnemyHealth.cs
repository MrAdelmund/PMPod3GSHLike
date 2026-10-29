using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 100;
    [SerializeField] bool isMeleeEnemy = true;
    [SerializeField] bool isRangedEnemy = false;
    [SerializeField] GameObject deathEffectPrefab;
    GameObject playerObject;
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(playerObject.transform.position, transform.position) > 20)
            Despawn();
    }
    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if (enemyHealth < 0)
        {
            Instantiate(deathEffectPrefab,transform.position,Quaternion.Euler(Vector3.zero));
            EnemySpawner.SubtractFromSpawnedEnemies();
            Destroy(gameObject);
        }
    }
    void Despawn()
    {
        EnemySpawner.AddToEnemySpawnQueue((isRangedEnemy) ? 1 : 0, (isMeleeEnemy) ? 0 : 1);
        EnemySpawner.SubtractFromSpawnedEnemies();
        Destroy(gameObject);
    }
}
