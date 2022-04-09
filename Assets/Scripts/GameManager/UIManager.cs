using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    private LevelManager _levelManager;
    public Button btnStart, btnNextLevel,btnTryAgain;

    public GameObject menuUI, inGameUI, endUI;
    public GameObject tryAgainUI;

    public Text levelText;
    void Start()
    {
        _levelManager = GameObject.FindWithTag("GameManager").GetComponent<LevelManager>();
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        SetBindings();
    }

    private void SetBindings()
    {
        btnStart.onClick.AddListener(call: () =>
        {
            _gameManager.StartGame();
            menuUI.SetActive(false);
            inGameUI.SetActive(true);

        }
        );
        btnNextLevel.onClick.AddListener(call: () =>
        {
            endUI.SetActive(false);
            _gameManager.StartNextGame();
            inGameUI.SetActive(true);
        }
        );
        btnTryAgain.onClick.AddListener(call: () =>
        {
            tryAgainUI.SetActive(false);
            _gameManager.StartCurrentGame();
            inGameUI.SetActive(true);
        }

        );
    }
    public void UpdateLevelText(int level)
    {
        levelText.text = "LEVEL " + (level + 1);
    }

    void Update()
    {
        
    }

    internal void EndLevel()
    {
        endUI.SetActive(true);
    }
    public void TryAgain()
    {
        inGameUI.SetActive(false);
    }
}
