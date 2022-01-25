using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // Using score tracker
using UnityEngine.SceneManagement; //Restart Button

public class GameManager : MonoBehaviour
{
    // Displays score
    private int score;
    // Displays UI elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    // Game is Active
    public bool isGameActive;
    // Enemy Spawns
    public GameObject[] BadMan;
    private float spawnRangeX = 25;
    private float spawnRangeZ = 20;
    private float startDelay = 4;
    private float spawnInterval = 2f;

    // Finds GameObjects
    private GameObject player;
    private GameObject enemy;
    private GameObject bullet;
    public Player health;

    void Start()
    {
        // Make check active first line
        isGameActive = true;
        // Set score text into game manager script
        score = 0;
        UpdateScore(0);
        UpdateHealth(0);

        // Spawns enemies
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
        // Finds GameObjects
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        bullet = GameObject.FindWithTag("Bullet");
        health = GameObject.Find("Player").GetComponent<Player>();
    }

    // Updates Score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    //Updates Player Health
    public void UpdateHealth(int healthLoss)
    {
        health.Health -= healthLoss;
        healthText.text = "Health: " + health.Health;
    }


    public void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX),
                                      0.5f, Random.Range(-spawnRangeZ, spawnRangeZ));
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
        // Destorys objects
        Destroy(player); 
        Destroy(enemy);
        Destroy(bullet);


    }
}
