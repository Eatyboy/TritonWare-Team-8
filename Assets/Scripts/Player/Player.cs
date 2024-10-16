using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActions ctrl;
    [SerializeField] private Rigidbody2D rb;

    // Core stats
    public float moveSpeed;
    public int maxHealth;
    public float damage; // Scalar multiplier for damage
    public float healthRegen; // Health regenerated per second
    public float lifeSteal; // Percentage of damage dealth healed as health
    public float projectileCount; // Number of additional projectiles that each attack fires
    public float cooldownReduction;

    public int currentHealth;

    // Active Abilities
    private IAbility ability1;
    private IAbility ability2;
    private float ability1CooldownTimer;
    private float ability2CooldownTimer;

    // Start is called before the first frame update
    void Awake() {
        ctrl = new InputActions();

        currentHealth = maxHealth;

        ability1 = GetComponent<Ability1>();
        ability2 = null;
        ability1CooldownTimer = 0;
        ability2CooldownTimer = 0;
    }

    private void OnEnable() {
        ctrl.Enable();
        ctrl.Gameplay.Ability1.performed += Ability1;
        ctrl.Gameplay.Ability2.performed += Ability2;
    }

    private void OnDisable() {
        ctrl.Gameplay.Ability1.performed -= Ability1;
        ctrl.Gameplay.Ability2.performed -= Ability2;
        ctrl.Disable();
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;

        currentHealth = (int)Mathf.Min(currentHealth + healthRegen * dt, maxHealth);

        if (ability1 != null && ability1CooldownTimer > 0) {
            ability1CooldownTimer -= dt;
            if (ability1CooldownTimer < 0) {
                ability1CooldownTimer = 0;
            }
        }

        if (ability2 != null && ability2CooldownTimer > 0) {
            ability2CooldownTimer -= dt;
            if (ability2CooldownTimer < 0) {
                ability2CooldownTimer = 0;
            }
        }
    }

    private void FixedUpdate() {
        float fdt = Time.fixedDeltaTime;
        Vector2 movement = ctrl.Gameplay.Move.ReadValue<Vector2>();

        rb.MovePosition(rb.position + moveSpeed * fdt * movement);
    }

    private void Ability1(InputAction.CallbackContext ctx) {
        if (ability1 == null) return;
        if (ability1CooldownTimer > 0) return;

        ability1.Activate();
        ability1CooldownTimer = ability1.GetCooldown();
    }

    private void Ability2(InputAction.CallbackContext ctx) {
        if (ability2 == null) return;
        if (ability2CooldownTimer > 0) return;

        ability2.Activate();
        ability2CooldownTimer = ability2.GetCooldown();
    }

    private int currentXP = 0;
    private int xpForNextLevel = 100;
    private int playerLevel = 1;

    public void AddXP(int xp)
    {
        currentXP += xp;
        if (currentXP >= xpForNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        currentXP = 0; // Reset XP
        xpForNextLevel += 50; // Increase XP requirement for the next level
        UpgradeStats(); // Increase player stats like health, speed, damage, etc.
    }

    private void UpgradeStats()
    {
        // Example stat upgrades
        moveSpeed += 1f;
        // Upgrade health, damage, etc.
    }
}
