using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelUpUI levelUpUI;

    private void Awake()
    {
        EventManager.RegisterToPlayerLevelUp(OnPlayerLevelUp);
    }

    private void OnPlayerLevelUp()
    {
        levelUpUI.gameObject.SetActive(true);
	}
}
