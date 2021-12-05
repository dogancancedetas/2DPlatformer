using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEnemyControl : MonoBehaviour
{
    Rigidbody2D enemyBody2D;
    public float enemySpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Duvara degiyor mu diye bak
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isOnGround)
            moveRight = !moveRight;

        enemyBody2D.velocity = (moveRight) ? new Vector2(enemySpeed, 0) : new Vector2(-enemySpeed, 0);
        transform.localScale = (moveRight) ? new Vector2(-1, 1) : new Vector2(1, 1);
    }
}
