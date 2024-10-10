using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelUpUI mLevelUpUI;
    [SerializeField] private PlayerLevel mPlayerLevel;

    private void Awake()
    {
        mPlayerLevel.mLevelUpEvent.AddListener(OnPlayerLevelUp);
    }

    private void OnPlayerLevelUp()
    {
        mLevelUpUI.gameObject.SetActive(true);
	}
}
