using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button menuButton;

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        EventManager.isPlayerDied = false;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        Debug.Log("Go To Menu");
        SceneManager.LoadScene(0);
	}
}
