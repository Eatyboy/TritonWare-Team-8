using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button exitButton;

    [SerializeField] GameObject SettingUI;

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);
        EventManager.isPlayerDied = false;
        Time.timeScale = 1;
    }

    public void SettingMenu()
    {
        SettingUI.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
