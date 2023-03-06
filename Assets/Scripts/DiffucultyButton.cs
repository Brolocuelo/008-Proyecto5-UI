using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffucultyButton : MonoBehaviour
{
    public int difficulty; // Dificultad (1 = easy, 2 = medium, 3 = hard)
    private Button _button;
    private GameManager gameManager;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
