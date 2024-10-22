using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Scrollbar BGMScrollBar;
    [SerializeField] private Text BGMValueText;

    [SerializeField] private Scrollbar SFXScrollBar;
    [SerializeField] private Text SFXValueText;

    AudioSource BGMSource;
    AudioSource SFXSource;

    private void Start()
    {
        BGMSource = GameManager.Instance.GetComponent<AudioSource>();
        SFXSource = SFXManager.Instance.audioSource;

        BGMScrollBar.value = BGMSource.volume;
        SFXScrollBar.value = SFXSource.volume;
    }

    private void OnEnable()
    {
        SelectionArrow.isControllable = false;
    }

    private void OnDisable()
    {
        SelectionArrow.isControllable = true;
    }

    private void Update()
    {
        SFXSource.volume = SFXScrollBar.value;
        SFXValueText.text = (int)(SFXScrollBar.value * 100) + " / 100";

        BGMSource.volume = BGMScrollBar.value;
        BGMValueText.text = (int)(BGMScrollBar.value * 100) + " / 100";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
		}
    }

}
