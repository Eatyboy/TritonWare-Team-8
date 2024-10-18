using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public static class PlayerAnimations
{ 
    public static string SPEEDUP_ANIM = "SpeedUp";
    public static string HEALTHUP_ANIM = "HealthUp";
    public static string DAMAGEUP_ANIM = "DamageUp";
}

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

    public Animator anim;

    // Invulnerability shit
    [SerializeField] private float invulnerabilityDuration = 1.5f; // Duration of invulnerability
    private float transparencyFlashSpeed = 0.2f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Awake() {
        ctrl = new InputActions();

        currentHealth = maxHealth;

        ability1 = GetComponent<Ability1>();
        ability2 = null;
        ability1CooldownTimer = 0;
        ability2CooldownTimer = 0;
        anim = GetComponent<Animator>();
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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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

    [SerializeField] private int currentXP = 0;
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

    public void TakeDamage(int damageAmount)
    {
        //Debug.Log("Hit!");
        //// If the enemy has a shield, remove it first
        //if (hasShield)
        //{
        //    RemoveShield();
        //    hasShield = false;
        //    return; // Ignore the damage for this hit
        //}

        // Apply damage to the enemy's health
        if (!isInvulnerable)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                //Die();
            }
            else
            {
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }

    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            // Flashing effect: toggle transparency on and off
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            yield return new WaitForSeconds(transparencyFlashSpeed);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(transparencyFlashSpeed);

            elapsedTime += transparencyFlashSpeed * 2; // Because of two WaitForSeconds
        }


        isInvulnerable = false;
    }

    private void Die()
    {
        // TODO
    }
}
