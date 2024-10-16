using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private InputActions ctrl;

    private void Awake() {
        ctrl = new InputActions();

        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void OnEnable() {
        ctrl.Enable();
    }

    private void OnDisable() {
        ctrl.Disable();
    }

    public Vector2 GetDirectionToMouse(Transform transform) {
        Vector2 objectPos = transform.position;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(ctrl.Gameplay.Mouse.ReadValue<Vector2>());
        return (worldMousePosition - objectPos).normalized;
    }
}
