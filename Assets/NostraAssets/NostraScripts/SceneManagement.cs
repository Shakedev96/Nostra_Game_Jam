using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Static instance of SceneManagement to ensure it persists across scenes
    public static SceneManagement Instance;

    [Header("Score Settings")]
    public int playerScore = 0;  // The player's score, carried over between scenes

    [Header("Scene Transition Settings")]
    [SerializeField] private float transitionDelay = 2f; // Time in seconds for level transition

    private void Awake()
    {
        // Singleton pattern to make sure there's only one instance of SceneManagement
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persistent between scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    // Call this method to add points to the player’s score
    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log("Score Updated: " + playerScore);
    }

    // Call this method to load the next level
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the build settings range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadLevelWithTransition(nextSceneIndex));
        }
        else
        {
            Debug.Log("Last level reached. Consider loading a main menu or ending screen.");
        }
    }

    // Coroutine to handle the level transition with a delay
    private System.Collections.IEnumerator LoadLevelWithTransition(int sceneIndex)
    {
        yield return new WaitForSeconds(transitionDelay); // Optional delay for a smooth transition effect
        SceneManager.LoadScene(sceneIndex);
    }
}

