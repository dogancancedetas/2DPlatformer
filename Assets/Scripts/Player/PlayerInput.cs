using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && player.isOnGround)
        {
            player.Jump();
            player.canDoubleJump = true;
            
        }
        else if (Input.GetButtonDown("Jump") && !player.isOnGround && player.canDoubleJump)
        {
            player.DoubleJump();
            player.canDoubleJump = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.ShootProjectile();
        }
    }
}
