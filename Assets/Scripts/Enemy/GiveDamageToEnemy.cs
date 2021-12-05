using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamageToEnemy : MonoBehaviour
{
    public float damage;
    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerItem")
        {
            enemyHealth.gotDamage = true;
        }
    }
}
