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
    public float spawnRate = 2f;
    public List<Vector3> targetPositionsInScene;
    public Vector3 randomPos;

    public TextMeshProUGUI scoreText;
    private int score;

    public GameObject gameOverPanel;

    private void Start()
    {
        isGameOver = false;
        StartCoroutine("SpawnRandomTarget");
        score = 0;
        scoreText.text = $"SCORE: {score}";
        gameOverPanel.gameObject.SetActive(false);
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
        scoreText.text = $"SCORE: {score}";
    }
}
