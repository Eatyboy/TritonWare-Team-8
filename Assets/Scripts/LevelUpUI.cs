using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    // Enable when player level up, and disable after selecting a perk
    [SerializeField] private Button mPerk1Btn;
    [SerializeField] private Button mPerk2Btn;
    [SerializeField] private Button mPerk3Btn;

    private void Awake()
    {
        mPerk1Btn.onClick.AddListener(ClickBtn1);
        mPerk2Btn.onClick.AddListener(ClickBtn2);
        mPerk3Btn.onClick.AddListener(ClickBtn3);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
	} 

    public void ClickBtn1()
    {
        Debug.Log("Choose Button 1");
        Deactive();
	}
    
    public void ClickBtn2()
    {
        Debug.Log("Choose Button 2");
        Deactive();
	}

    public void ClickBtn3()
    {
        Debug.Log("Choose Button 3");
        Deactive();
	}

}
