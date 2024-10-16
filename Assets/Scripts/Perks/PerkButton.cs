using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkButton : MonoBehaviour
{
    [SerializeField] private IPerk mPerk;
    private Button mButton;
    private PerkProvider perkProvider;
    [SerializeField] private Player mPlayer;

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
        EventManager.InvokeEvent(EventManager.Events.ChoosePerk);
	}

    private void UpdatePerk()
    {
        mPerk = perkProvider.GetComponent<PerkProvider>().GetRandomPerk();
        gameObject.GetComponentInChildren<TMP_Text>().text = mPerk.perkName;
    }
}
