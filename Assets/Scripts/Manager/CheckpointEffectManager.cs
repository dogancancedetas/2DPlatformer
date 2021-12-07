using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEffectManager : MonoBehaviour
{
    public GameObject idle;
    public GameObject playerIn;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            idle.SetActive(false);
            playerIn.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            idle.SetActive(true);
            playerIn.SetActive(false);
        }
    }
}
