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

    [Tooltip("Karakterin ne kadar yuksege ziplayacagini belirler.")]
    [Range(0,1500)]
    public float jumpForce;


    //Yeri bulma
    [Tooltip("Karakterin yere degip degmedigini kontrol eder.")]
    public bool isOnGround;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    [Tooltip("Yerin ne oldugunu belirler.")]
    public LayerMask groundLayer;


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
    }

    
    // FixedUpdate: Framerate'den bagimsiz olarak calisir. Fizik ile ilgili kodlari buraya yazin.
    void FixedUpdate()
    {
        isOnGround =  Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float h = Input.GetAxis("Horizontal");
       
        body2D.velocity = new Vector2(h * playerSpeed, body2D.velocity.y);
    }

    public void Jump()
    {
        body2D.AddForce(new Vector2(0, jumpForce));
        isOnGround = false;
    }
}
