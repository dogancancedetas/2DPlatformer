using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxEnemyHealth;
    public float currentEnemyHealth;
    internal bool gotDamage;
    public float damage;
    Transform deathParticle;
    SpriteRenderer graph;
    CircleCollider2D circle2D;
    BoxCollider2D box2D;
    Player player;
    Rigidbody2D body2D;
    AudioClip auKill;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        player = FindObjectOfType<Player>();
        graph = GetComponent<SpriteRenderer>();
        circle2D = GetComponent<CircleCollider2D>();
        box2D = GetComponent<BoxCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        deathParticle = transform.Find("DeathParticle");
        deathParticle.gameObject.SetActive(false);
        auKill = Resources.Load("SoundEffects/Kill") as AudioClip;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemyHealth <= 0)
        {
            graph.enabled = false;
            circle2D.enabled = false;
            box2D.enabled = false;
            deathParticle.gameObject.SetActive(true);
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            Destroy(gameObject,1);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerItem" && player.canDamage)
        {
            currentEnemyHealth -= damage;
            player.auSource.PlayOneShot(auKill);

        }
    }
}
