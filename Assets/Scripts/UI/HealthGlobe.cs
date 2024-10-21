using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthGlobe : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;
    [SerializeField] private GameObject healthText;
    [SerializeField] private TextMeshProUGUI healthTextTMP;

    private bool hover = false;

    // Update is called once per frame
    void Update()
    {
        slider.value = player.currentHealth / player.maxHealth;
        if (hover)
        {
            healthTextTMP.text = ((int)player.currentHealth).ToString() + " / " + ((int)player.maxHealth).ToString();
        }
    }

    private void OnMouseEnter()
    {
        healthText.SetActive(true);
        hover = true;
    }

    private void OnMouseExit()
    {
        healthText.SetActive(false);
        hover = false;
    }
}
