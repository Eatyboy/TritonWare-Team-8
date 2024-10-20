using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer playerRenderer;

    private Vector3 offset;

    private void Start()
    {
        offset = 1.2f * playerRenderer.bounds.extents.y * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.tookDamageRecently) gameObject.SetActive(true);
        else gameObject.SetActive(false);
        
        transform.position = player.transform.position + offset;
        slider.value = player.currentHealth / player.maxHealth;
    }
}
