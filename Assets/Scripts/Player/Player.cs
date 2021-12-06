using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //parantez icine yazdigimiz component'i zorunlu kilar kaldirmayi engeller. Bulunan scripti eklerseniz component'i ekler
public class Player : MonoBehaviour
{
    //Rigidbody2D
    Rigidbody2D body2D;

    //Collider2D
    CircleCollider2D circle2D;
    BoxCollider2D box2D;

    //Animator controller animasyonlari kontrol eder
    Animator playerAnimController;

    /// <summary>
    /// bu var'lar karakterin hizini ve ziplama gucunu belirler
    /// </summary>
    [Tooltip("Karakterin ne kadar hizli gidecegini belirler.")] // uzerine gelindiginde bir tooltip cikartir
    [Range(0, 20)]
    public float playerSpeed;

    //Ziplama
    [Tooltip("Karakterin ne kadar yuksege ziplayacagini belirler.")]
    [Range(0, 1500)]
    public float jumpForce;

    //Double Jump
    [Tooltip("Karakterin 2. ziplamada ne kadar yuksege ziplayacagini belirler.")]
    [Range(0, 30)]
    public float doubleJumpForce;
    internal bool canDoubleJump;

    internal bool canDamage;

    //Karakteri dondurmek
    bool facingRight = true;

    //Yeri bulma
    [Tooltip("Karakterin yere degip degmedigini kontrol eder.")]
    public bool isOnGround;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    [Tooltip("Yerin ne oldugunu belirler.")]
    public LayerMask groundLayer;

    //Oyuncu cani
    internal int maxHealth = 100;
    public int currentHealth;
    internal bool isHurt;
    public float knockBackForce;
    GiveDamage damage;

    //Oyuncuyu öldür
    internal bool isDead;
    public float deadForce;


    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody ayarlari
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 5;
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        //Collider ayarlari
        box2D = GetComponent<BoxCollider2D>();
        circle2D = GetComponent<CircleCollider2D>();

        //GrouncCheck'i bul.
        groundCheck = transform.Find("GroundCheck");

        //Animator'u al.
        playerAnimController = GetComponent<Animator>();

        //hp'i maksimum hp'e esitle
        damage = FindObjectOfType<GiveDamage>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateAnimations();
        ReduceHealth();
        isDead = currentHealth <= 0;

        if (isDead)
            KillPlayer();

        //Eger hp max hp'dan yuksekse hp'i max hp'ye esitle
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    // FixedUpdate: Framerate'den bagimsiz olarak calisir. Fizik ile ilgili kodlari buraya yazin.
    void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float h = Input.GetAxis("Horizontal");
        body2D.velocity = new Vector2(h * playerSpeed, body2D.velocity.y);
        Flip(h);

        if (isOnGround)
        {
            canDamage = false;
        }
    }

    public void Jump()
    {
        body2D.AddForce(new Vector2(0, jumpForce));
    }
    public void DoubleJump()
    {
        //Y yönünde ani bir kuvvet uygular.
        body2D.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);
        canDamage = true;
    }

    void Flip(float h)
    {
        if (h > 0 && !facingRight || h < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector2 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void UpdateAnimations()
    {
        playerAnimController.SetFloat("VelocityX", Mathf.Abs(body2D.velocity.x));
        playerAnimController.SetBool("isOnGround", isOnGround);
        playerAnimController.SetFloat("VelocityY", body2D.velocity.y);
        playerAnimController.SetBool("isDead", isDead);
        if (!isDead && isHurt)
            playerAnimController.SetTrigger("isHurt");
    }

    //Hp azaltma fonksiyonu
    void ReduceHealth()
    {
        if (isHurt)
        {
            //Eger hp 100 ise o zaman hp'den zarar kadar cikar.
            //hp-damage = newhp
            currentHealth -= damage.damage;
            isHurt = false;

            //Eger havadatsa sol veya sag ve dikey yonde guc uygula
            if (facingRight && !isOnGround)
            {
                body2D.AddForce(new Vector2(-knockBackForce, 10), ForceMode2D.Impulse);
            }
            else if (!facingRight && !isOnGround)
            {
                body2D.AddForce(new Vector2(knockBackForce, 10), ForceMode2D.Impulse);
            }

            //Eger yerdeysek sol veya sag yonde guc uygula
            if (facingRight && isOnGround)
            {
                body2D.AddForce(new Vector2(-knockBackForce, 0), ForceMode2D.Force);
            }
            else if (!facingRight && isOnGround)
            {
                body2D.AddForce(new Vector2(knockBackForce, 0), ForceMode2D.Force);
            }
        }
    }

    //oyuncuyu oldurme fonksiyonu
    void KillPlayer()
    {
        isHurt = false;
        body2D.AddForce(new Vector2(0, deadForce), ForceMode2D.Impulse);
        body2D.drag += Time.deltaTime * 50;
        deadForce -= Time.deltaTime * 20;
        body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        box2D.enabled = false;
        circle2D.enabled = false;
    }


}
