using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDamage : MonoBehaviour
{
    int damage;
    float timer = 0;
    BulletData bulletData;
    private void Start()
    {
        bulletData = GetComponent<BulletData>();
        if (bulletData == null)
            bulletData = GetComponentInParent<BulletData>();
        damage = bulletData.bulletDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (bulletData.isLightsaber)
        {
            timer += Time.deltaTime;
            if (timer > 0.2)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}