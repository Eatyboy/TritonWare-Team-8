using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button menuButton;

    [SerializeField] GameObject SettingUI;

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        EventManager.InvokeEvent(EventManager.Events.ResumeGame);
    }

    public void SettingMenu()
    {
        SettingUI.SetActive(true);
    }

    public void GoToMenu()
    {
        Debug.Log("Go To Menu");
        SceneManager.LoadScene(0);
	}
}
