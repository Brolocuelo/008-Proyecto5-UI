using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifeTime = 2f;

    private GameManager gameManager;

    public int points;

    public GameObject explosionParticle;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Destroy(gameObject, lifeTime);
    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameOver)
        {
            if (gameObject.CompareTag("Bad"))
            {
                if (gameManager.hasPowerShield)
                {
                    gameManager.hasPowerShield = false;
                }
                else
                {
                    gameManager.MinusLife();
                }
            }
            else if (gameObject.CompareTag("Good"))
            {
                gameManager.UpdateScore(points);
            }
            else if (gameObject.CompareTag("Shield"))
            {
                gameManager.hasPowerShield = true;
            }
            Instantiate(explosionParticle, transform.position,explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        gameManager.targetPositionsInScene.Remove(transform.position);
    }
}
