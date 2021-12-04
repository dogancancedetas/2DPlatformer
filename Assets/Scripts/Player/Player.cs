using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //parantez icine yazdigimiz component'i zorunlu kilar kaldirmayi engeller. Bulunan scripti eklerseniz component'i ekler
public class Player : MonoBehaviour
{
    //Rigidbody2D
    Rigidbody2D body2D;

    /// <summary>
    /// bu var'lar karakterin hizini ve ziplama gucunu belirler
    /// </summary>
    [Tooltip("Karakterin ne kadar hizli gidecegini belirler.")] // uzerine gelindiginde bir tooltip cikartir
    [Range(0, 20)] 
    public float playerSpeed;
    
    //Zýplama
    [Tooltip("Karakterin ne kadar yuksege ziplayacagini belirler.")]
    [Range(0,1500)]
    public float jumpForce;

    //Double Jump
    [Tooltip("Karakterin 2. ziplamada ne kadar yuksege ziplayacagini belirler.")]
    [Range(0, 30)]
    public float doubleJumpForce;

    internal bool canDoubleJump;

    //Karakteri döndürmek
    bool facingRight = true;

    //Yeri bulma
    [Tooltip("Karakterin yere degip degmedigini kontrol eder.")]
    public bool isOnGround;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    [Tooltip("Yerin ne oldugunu belirler.")]
    public LayerMask groundLayer;

    //Animator controller animasyonlari kontrol eder
    Animator playerAnimController;

    //Oyuncu cani
    internal int maxHealth = 100;
    public int currentHealth;
    internal bool isHurt;
    Damage damage;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody ayarlari
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 5;
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        //GrouncCheck'i bul.
        groundCheck = transform.Find("GroundCheck");

        //Animator'u al.
        playerAnimController = GetComponent<Animator>();

        //hp'i maksimum hp'e esitle
        damage = FindObjectOfType<Damage>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateAnimations();
        ReduceHealth();

        //Eger hp max hp'dan yuksekse hp'i max hp'ye esitle
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        
    }

    // FixedUpdate: Framerate'den bagimsiz olarak calisir. Fizik ile ilgili kodlari buraya yazin.
    void FixedUpdate()
    {
        
        isOnGround =  Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float h = Input.GetAxis("Horizontal");
        body2D.velocity = new Vector2(h * playerSpeed, body2D.velocity.y);
        Flip(h);
    }

    public void Jump()
    {
        body2D.AddForce(new Vector2(0, jumpForce));
        
    }
    public void DoubleJump()
    {
        //Y yönünde ani bir kuvvet uygular.
        body2D.AddForce(new Vector2(0, doubleJumpForce), ForceMode2D.Impulse);

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
        playerAnimController.SetFloat("VelocityX",Mathf.Abs(body2D.velocity.x));
        playerAnimController.SetBool("isOnGround", isOnGround);
        playerAnimController.SetFloat("VelocityY", body2D.velocity.y);
        if(isHurt)
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
        }
    }

}
