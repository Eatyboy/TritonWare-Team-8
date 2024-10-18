using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    public static DamagePopupManager Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public void NewPopup(float damage, Vector2 pos)
    {
        GameObject newPopup = Instantiate(popup, transform, true);
        DamagePopup tmp = newPopup.GetComponent<DamagePopup>();
        tmp.Init(pos, damage);
    }
}
