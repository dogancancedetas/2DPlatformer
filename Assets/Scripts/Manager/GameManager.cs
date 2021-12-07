using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;

    //UI
    public Slider healthBar;
    public Text points;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        healthBar.maxValue = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        points.text ="Coins: " + player.currentPoint.ToString();
        if (player.isDead)
        {
            Invoke("RestartGame", 2); //string içindeki fonksiyonu çaðýrmadan önce verilen deger kadar bekler
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.value = player.currentHealth;

        if (player.currentHealth <= 0)
            healthBar.minValue = 0;
    }

    public void RestartGame()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);

        player.RecoverPlayer();
    }
}
