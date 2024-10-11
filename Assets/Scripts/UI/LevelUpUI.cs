using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    // Enable when player level up, and disable after selecting a perk
    [SerializeField] private Button mPerk1Btn;
    [SerializeField] private Button mPerk2Btn;
    [SerializeField] private Button mPerk3Btn;

    [SerializeField] private GameObject player;
    [SerializeField] private List<IPerk> availablePerks;

    private void Awake()
    {
        mPerk1Btn.onClick.AddListener(ApplyPerk);
        mPerk2Btn.onClick.AddListener(ApplyPerk);
        mPerk3Btn.onClick.AddListener(ApplyPerk);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
	} 

    public void ApplyPerk()
    {
        GetRandomPerk().Apply(player);
        Deactive();
	}
    
    private IPerk GetRandomPerk()
    {
        IPerk thePerk = availablePerks[Random.Range(0, availablePerks.Count)];
        return thePerk;
	}

}
