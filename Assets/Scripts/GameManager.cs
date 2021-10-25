using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool _startGame;
    public bool StartGame => _startGame;
    private void Awake()
    {
        Instance = this;
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadSceneAsync(0);
    }

    public void StartTheGame()
    {
        _startGame = true;
    }
}
