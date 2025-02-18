using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to access the Text component

public class PlayerCollectibles : MonoBehaviour
{
    // Reference to the UI text component where the coin count will be displayed
    public TextMeshProUGUI coinText;
    private int coinCount = 0; // Counter to keep track of the collected coins

    void Start()
    {
        // Initialize the coin count display at the start of the game
        UpdateCoinText();
    }

    // This method is called when the player collides with other objects
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is on the "Collectibles" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectibles"))
        {
            // Increase the coin count by 1
            coinCount++;

            // Update the UI to show the new coin count
            UpdateCoinText();

            // Destroy the collectible GameObject to simulate collecting it
            Destroy(other.gameObject);
        }
    }

    // Method to update the text component with the current coin count
    void UpdateCoinText()
    {
        // Set the text property of the coinText UI component to display the coin count
        coinText.text = " " + coinCount;
    }
}

