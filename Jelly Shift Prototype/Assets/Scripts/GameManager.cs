using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] obstaclePrefabs;
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public bool gameOver = false;
    public bool hitBrake = false;
    public float obstacleDelay = 1;
    public int score = 0;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public Button reloadButton;
    public int health = 3;

    // This method is called by unity on object creation and is used for Singleton management
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
        } else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlayer();
        StartCoroutine(ObstacleSpawnRoutine());
        DisplayHealth();
        DisplayScore();
        reloadButton.gameObject.SetActive(false);
    }

    // This method instantiates the player prefab
    void InstantiatePlayer()
    {
        Instantiate(playerPrefab, new Vector3(0, 2f, 0), playerPrefab.transform.rotation);
    }

    // This method instantiates an obstacle in the scene at the given spawn point
    void InstantiateObstacle()
    {
        int tempIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject tempObstacle = Instantiate(obstaclePrefabs[tempIndex], spawnPoint.transform.position, obstaclePrefabs[tempIndex].transform.rotation);
    }

    // This method spawns obstacles after random intervals till the game is over
    IEnumerator ObstacleSpawnRoutine()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(obstacleDelay);
            InstantiateObstacle();
            obstacleDelay = Random.Range(1, 3);
        }
    }

    // This method adds to the total score
    public void AddScore(int score)
    {
        this.score += score;
        DisplayScore();
    }

    // This method displays health on the ui
    public void DisplayHealth()
    {
        
        if(health > 0)
        {
            healthText.text = "Health: " + health;
        } else
        {
            healthText.text = "You are dead mate.";
        }
    }

    // This method displays score on the ui
    public void DisplayScore()
    {
        scoreText.text = "Score: " + score;
    }

    // This method subtracts health
    public void GetDamage()
    {
        health--;
        DisplayHealth();
        CheckDeath();
    }

    // This method checks if the player has died
    void CheckDeath()
    {  
        if (health < 1)
        {
            GameOver();
        }
    }

    // This method reloads the game
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    // This method sets the gameover variable to true and enables reload button
    public void GameOver()
    {
        gameOver = true;
        reloadButton.gameObject.SetActive(true);
    }
}
