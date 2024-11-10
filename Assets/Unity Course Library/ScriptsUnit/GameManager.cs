using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    

    private int score = 0;
    
    public bool isGameActive;
   public float spawnRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        isGameActive = true;
        
        UpdateScore(0);

        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0,targets.Count);

            Instantiate(targets[index]);

            //UpdateScore(5);
            //UpdatedScore(5);
        }


        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = "Score: " + score;

    }

   /*  public void UpdatedScore(int scoreTODeduct)
    {
        score -= scoreTODeduct;

        scoreText.text = "Score: " + score;
    } */

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
