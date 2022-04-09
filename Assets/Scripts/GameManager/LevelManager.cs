using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;

    public int currentLevel;

    private PlayerMove _player;
    private CameraFollow _camera;

    private Vector3 playerDefaultPosition;
    private Vector3 cameraDefaultPosition;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();

        playerDefaultPosition = _player.transform.position;
        cameraDefaultPosition = _camera.transform.position;

    }

    public void StartLevel()
    {
        levels[currentLevel % levels.Length].gameObject.SetActive(true);
        _player.transform.position = playerDefaultPosition;
        _camera.transform.position = cameraDefaultPosition;
    }

    public void StartNextLevel()
    {
        levels[currentLevel % levels.Length].ResetLevel();
        levels[currentLevel % levels.Length].gameObject.SetActive(false);
        currentLevel++;
        StartLevel();
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.Save();
    }
    public void StartLevelAgain()
    {
        levels[currentLevel % levels.Length].ResetLevel();
        levels[currentLevel % levels.Length].gameObject.SetActive(true);
        _player.transform.position = playerDefaultPosition;
        _camera.transform.position = cameraDefaultPosition;
    }

    public void EndLevel()
    {

    }
}
