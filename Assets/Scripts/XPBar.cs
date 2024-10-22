using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerLevel playerLevel;

    // Update is called once per frame
    void Update()
    {
        float offsetXP = playerLevel.GetRequiredExp(playerLevel.CurrentLevel - 1);
        float currXP = playerLevel.CurrentExp - offsetXP;
        float requiredXP = playerLevel.RequiredExp - offsetXP; 
        slider.value = currXP / requiredXP;
    }
}
