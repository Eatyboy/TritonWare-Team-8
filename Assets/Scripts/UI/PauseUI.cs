using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button menuButton;

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        EventManager.InvokeEvent(EventManager.Events.ResumeGame);
    }

    public void SettingMenu()
    {
        Debug.Log("open setting menu");
    }

    public void GoToMenu()
    {
        Debug.Log("Go To Menu");
        SceneManager.LoadScene(0);
	}
}
