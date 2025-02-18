using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartHealth : MonoBehaviour
{
  


    private int maxHealth = 3;
    private int currentHealth;
    public Image[] hearts; // Assign heart images in the Inspector
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Ensure your enemies have the "Enemy" tag
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
        }

        if (currentHealth <= 0)
        {
            // Implement game over logic here
            //Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
            hearts[i].enabled = i < maxHealth;
        }
    }
}

