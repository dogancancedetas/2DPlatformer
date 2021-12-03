using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //parantez içine yazdýðýmýz component'ý zorunlu kýlar kaldýrmayý engeller. Bulunan scripti eklerseniz component'ý ekler
public class Player : MonoBehaviour
{
    
    Rigidbody2D body2D;
    public float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 50;
        body2D.freezeRotation = true;
    }

    
    // FixedUpdate: Framerate'den baðýmsýz olarak çalýþýr. Fizik ile ilgili kodlarý buraya yazýn.
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
       
        body2D.velocity = new Vector2(h * playerSpeed, 0);
    }
}
