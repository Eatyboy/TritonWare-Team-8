using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelUpUI levelUpUI;
    [SerializeField] private PauseUI pauseUI;
    [SerializeField] private GameOverUI gameoverUI;

    private void OnEnable()
    {
        EventManager.RegisterToEvent(EventManager.Events.PlayerLevelUp, OnPlayerLevelUp);
        EventManager.RegisterToEvent(EventManager.Events.PauseGame, OnPauseGame);
        EventManager.RegisterToEvent(EventManager.Events.ResumeGame, OnResumeGame);
        EventManager.RegisterToEvent(EventManager.Events.PlayerDie, OnPlayerDie);
    }

    private void OnDisable()
    {
        EventManager.UnregisterFromEvent(EventManager.Events.PlayerLevelUp, OnPlayerLevelUp);
        EventManager.UnregisterFromEvent(EventManager.Events.PauseGame, OnPauseGame);
        EventManager.UnregisterFromEvent(EventManager.Events.ResumeGame, OnResumeGame);
        EventManager.UnregisterFromEvent(EventManager.Events.PlayerDie, OnPlayerDie);
    }

    private void OnPlayerLevelUp()
    {
        levelUpUI.gameObject.SetActive(true);
	}

    private void OnPauseGame()
    {
        if (!EventManager.isGamePaused && !EventManager.isPlayerDied)
        {
            pauseUI.gameObject.SetActive(true);
            EventManager.isGamePaused = true;
            Time.timeScale = 0;
        }
    }

    private void OnResumeGame()
    {
        if (EventManager.isGamePaused && !EventManager.isPlayerDied)
        {
            pauseUI.gameObject.SetActive(false);
            EventManager.isGamePaused = false;
            Time.timeScale = 1;
        }
    }

    private void OnPlayerDie()
    {
        EventManager.isPlayerDied = true;
        gameoverUI.gameObject.SetActive(true);
        Time.timeScale = 0;
	}
}
