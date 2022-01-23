using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // Using score tracker
using UnityEngine.SceneManagement; //Restart Button
using UnityEngine.UI; // Show button on game over

public class GameManager : MonoBehaviour
{
    // Displays score
    private int score;
    // Displays UI elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    // Game is Active
    public bool isGameActive;
    // Enemy Spawns
    public GameObject[] BadMan;
    private float spawnRangeX = 15;
    private float spawnRangeZ = 20;
    private float startDelay = 4;
    private float spawnInterval = 5f;

    void Start()
    {
        // Make check active first line
        isGameActive = true;
        // Set score text into game manager script
        score = 0;
        UpdateScore(0);

        // Spawns enemies
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
    }

    // Updates Score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }


    public void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
                                      0.3f, Random.Range(-spawnRangeZ, spawnRangeZ));
        int badPeeps = Random.Range(0, BadMan.Length);
        Instantiate(BadMan[badPeeps], spawnPos, BadMan[badPeeps].transform.rotation);
    }

    private void Update()
    {
        if (isGameActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Reloads Scene
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) //Returns to Title
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void GameOver()
    {
        // Show game over texts
        gameOverText.gameObject.SetActive(true);        
        // Stops game
        isGameActive = false;
        // Cancels enemy spawning
        CancelInvoke("SpawnEnemy");

      


    }
}
