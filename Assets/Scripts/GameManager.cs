using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;

    private float minX = -3.75f;
    private float minY = -3.75f;
    private float distanceBetweenSauares = 2.5f;

    public bool isGameOver;
    public List<Vector3> targetPositionsInScene;
    private Vector3 randomPos;

    public TextMeshProUGUI scoreText;
    private float spawnRate = 2f;
    private int score; // puntuación total;

    public GameObject gameOverPanel;
    public GameObject startGamePanel;

    public TextMeshProUGUI lifesText;
    private int lives = 3;

    public bool hasPowerShield;

    private void Start()
    {
        startGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minX + Random.Range(0, 4) *
         distanceBetweenSauares;
        float spawnPosY = minY + Random.Range(0, 4) *
         distanceBetweenSauares;
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    private IEnumerator SpawnRandomTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targetPrefabs.Length);
            randomPos = RandomSpawnPosition();
            while (targetPositionsInScene.Contains(randomPos))
            {
                randomPos = RandomSpawnPosition();
            }
            Instantiate(targetPrefabs[randomIndex], randomPos,
             targetPrefabs[randomIndex].transform.rotation);
            targetPositionsInScene.Add(randomPos);

        }
    }

    public void UpdateScore(int newPoints)
    {
        score += newPoints;
        scoreText.text = $"SCORE: \n{score}";
    }

    public void StartGame(int difficulty)
    {
        isGameOver = false;
        score = 0;
        lives = 3;
        lifesText.text = $"Lives: \n{lives}";
        UpdateScore(0);
        spawnRate /= difficulty;
        StartCoroutine(SpawnRandomTarget());
        startGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void MinusLife() 
    {
        lives--;
        lifesText.text = $"Lives: \n{lives}";
        if (lives <= 0)
        {
            GameOver();
        }
    }
}
