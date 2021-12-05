using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int maxEnemyHealth = 100;
    internal float currentEnemyHealth;
    internal bool gotDamage;

    GiveDamageToEnemy giveDamage;

    // Start is called before the first frame update
    void Start()
    {
        giveDamage = GetComponent<GiveDamageToEnemy>();
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (gotDamage)
        {
            currentEnemyHealth -= giveDamage.damage;
            gotDamage = false;
            Debug.Log("Ouch");
        }

        
    }
}
