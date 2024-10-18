using UnityEngine;
using System.Collections.Generic;

public class LevelUpUI : MonoBehaviour
{
    // Enable when player level up, and disable after selecting a perk
    [SerializeField] private List<PerkButton> mPerkButtons;

    private void Awake()
    {
        EventManager.RegisterToEvent(EventManager.Events.ChoosePerk, OnChoosePerk);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
	}

    public void OnChoosePerk()
    {
        Deactive();
        Time.timeScale = 1;
	}
}
