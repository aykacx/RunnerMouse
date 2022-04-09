using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Pause,
    End,
    Dead
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState;
    private LevelManager _levelManager;
    private UIManager _uiManager;
    public PlayerMove _player;
    void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        _uiManager = GameObject.FindWithTag("MainUI").GetComponent<UIManager>();
        currentGameState = GameState.Pause;
    }

    public void StartGame()
    {
        currentGameState = GameState.Start;
        _uiManager.UpdateLevelText(_levelManager.currentLevel);
        _player.animator.Play(PlayerMove.runString);
        _levelManager.StartLevel();
    }

    public void StartNextGame()
    {
        currentGameState = GameState.Start;
        _levelManager.StartNextLevel();
        _uiManager.UpdateLevelText(_levelManager.currentLevel);
    }
    public void StartCurrentGame()
    {
        currentGameState = GameState.Start;
        _levelManager.StartLevelAgain();

    }

    internal void EndGame()
    {
        _levelManager.EndLevel();
        _uiManager.EndLevel();
        currentGameState = GameState.End;
    }
    public void KillPlayer()
    {
        _levelManager.EndLevel();
        _uiManager.TryAgain();
        currentGameState = GameState.Dead;
    }
    public void SetPlayer(PlayerMove playerMove)
    {
        _player = playerMove;
    }
}
