using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    // Enable when player level up, and disable after selecting a perk
    [SerializeField] PlayerLevel mPlayerLevel;
    [SerializeField] private Button mPerk1Btn;
    [SerializeField] private Button mPerk2Btn;
    [SerializeField] private Button mPerk3Btn;

    public void Deactive()
    {
        gameObject.SetActive(false);
	} 

}
