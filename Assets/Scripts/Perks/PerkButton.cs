using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkButton : MonoBehaviour
{
    [SerializeField] private IPerk mPerk;
    [SerializeField] private Player mPlayer;
    [SerializeField] private Image mPerkImage;
    private Button mButton;
    private PerkProvider perkProvider;

    private void Awake()
    {
        perkProvider = FindObjectOfType<PerkProvider>();
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(ApplyPerk);
        EventManager.RegisterToEvent(EventManager.Events.PlayerLevelUp, UpdatePerk);
    }

    private void Start()
    {
        UpdatePerk();
    }

    private void ApplyPerk()
    {
        mPerk.Apply(mPlayer);
        mPerk.picked = true;
        EventManager.InvokeEvent(EventManager.Events.ChoosePerk);
	}

    private void UpdatePerk()
    {
        Debug.Log("Update perk");
        mPerk = perkProvider.GetComponent<PerkProvider>().GetRandomPerk();
        gameObject.GetComponentInChildren<TMP_Text>().text = mPerk.perkName;
        mPerkImage.sprite = mPerk.sprite;
    }
}
