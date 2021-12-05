using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemyControl : MonoBehaviour
{
    Rigidbody2D enemyBody2D;
    public float enemySpeed;
    EnemyHealth enemyHealth;
    Animator flowerEnemyAnim;

    //Duvari bulma
    [Tooltip("Karakterin duvara degip degmedigini kontrol eder.")]
    bool isOnGround;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    [Tooltip("Duvarin ne oldugunu belirler.")]
    public LayerMask groundLayer;
    public bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        enemyBody2D = GetComponent<Rigidbody2D>();
        flowerEnemyAnim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.currentEnemyHealth <= 0)
        {
            flowerEnemyAnim.SetBool("isDead", true);
            Destroy(gameObject, 1);
           
        }
        //Duvara degiyor mu diye bak
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isOnGround)
            moveRight = !moveRight;

        enemyBody2D.velocity = (moveRight) ? new Vector2(enemySpeed, 0) : new Vector2(-enemySpeed, 0);
        transform.localScale = (moveRight) ? new Vector2(-1, 1) : new Vector2(1, 1);


    }
}
