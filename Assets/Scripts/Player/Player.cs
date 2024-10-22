using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Vector3 lastMovementDirection = Vector3.up;

    // Core stats
    public float moveSpeed;
    public float maxHealth;
    public float damage; // Scalar multiplier for damage
    public float healthRegen; // Health regenerated per second
    public float lifeSteal; // Percentage of damage dealth healed as health
    public float projectileCount; // Number of additional projectiles that each attack fires
    public float cooldownReduction;
    public float luck;

    public float currentHealth;

    public bool tookDamageRecently = false;
    [SerializeField] private float damageRecencyDuration;
    private float damageRecencyTimer = 0;
    [SerializeField] private HealthBar healthBar;

    // Active Abilities
    public IAbility active1;
    public IAbility active2;
    private float active1CDTimer;
    private float active2CDTimer;

    // Passive Abilities
    public int maxPassiveSlots;
    public IAbility[] passives;
    private float[] passiveCDTimers;
    private int passiveCount;

    public Animator anim;

    // Invulnerability shit
    [SerializeField] private float invulnerabilityDuration = 1.5f; // Duration of invulnerability
    private float transparencyFlashSpeed = 0.2f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Awake() {
        ctrl = new InputActions();

        currentHealth = maxHealth;

        active1 = null;
        active2 = null;

        active1CDTimer = 1;
        active2CDTimer = 1;

        passives = new IAbility[maxPassiveSlots];
        passiveCDTimers = new float[maxPassiveSlots];
        passiveCount = 0;

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

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        damageRecencyTimer = Mathf.Max(damageRecencyTimer - dt, 0);
        if (damageRecencyTimer <= 0) {
            tookDamageRecently = false; 
            healthBar.gameObject.SetActive(false);
        }

        if (active1 != null && active1CDTimer > 0) {
            active1CDTimer -= dt;
            if (active1CDTimer < 0) {
                active1CDTimer = 0;
            }
        }

        if (active2 != null && active2CDTimer > 0) {
            active2CDTimer -= dt;
            if (active2CDTimer < 0) {
                active2CDTimer = 0;
            }
        }

        for (int i = 0; i < passiveCount; ++i)
        {
            passiveCDTimers[i] -= dt;
            if (passiveCDTimers[i] <= 0)
            {
                passives[i].Activate();
                passiveCDTimers[i] = passives[i].GetCooldown();
                SFXManager.Instance.PlayRandomSound(SFXManager.SFX.ENEMY_SHOOT);
            }
        }

        Vector2 currentMovement = ctrl.Gameplay.Move.ReadValue<Vector2>();
        if (currentMovement != Vector2.zero) lastMovementDirection = currentMovement.normalized;

        HandlePauseInput();
        if (Input.GetKeyDown(KeyCode.O))
        {
            Die();
		}
    }

    private void FixedUpdate() {
        float fdt = Time.fixedDeltaTime;
        Vector2 movement = ctrl.Gameplay.Move.ReadValue<Vector2>();

        rb.MovePosition(rb.position + moveSpeed * fdt * movement);
    }

    private void HandlePauseInput()
    { 
	    if (Input.GetKeyDown(KeyCode.P))
        {
            if (!EventManager.isGamePaused)
            {
                EventManager.InvokeEvent(EventManager.Events.PauseGame);
            }
            else
            { 
                EventManager.InvokeEvent(EventManager.Events.ResumeGame);
			}
        }
	}

    private void Ability1(InputAction.CallbackContext ctx) {
        if (active1 == null) return;
        if (active1CDTimer > 0) return;

        active1.Activate();
        active1CDTimer = active1.GetCooldown();
    }

    private void Ability2(InputAction.CallbackContext ctx) {
        if (active2 == null) return;
        if (active2CDTimer > 0) return;

        active2.Activate();
        active2CDTimer = active2.GetCooldown();
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
            SFXManager.Instance.PlayRandomSound(SFXManager.SFX.PLAYER_HIT);

            tookDamageRecently = true;
            damageRecencyTimer = damageRecencyDuration;
            healthBar.gameObject.SetActive(true);

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

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<IEnemy>().getDMG());
        }
    }

    private void Die()
    {
        EventManager.InvokeEvent(EventManager.Events.PlayerDie);
    }

    public Vector3 GetMovementDirection()
    {
        // Get the movement direction from the input system
        return ctrl.Gameplay.Move.ReadValue<Vector2>();
    }

    public Vector3 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }

    public void AddPassive(IAbility passive)
    {
        if (passiveCount == maxPassiveSlots)
        {
            Debug.LogError("Attempted to exceed max passive slots");
            return;
        }

        passives[passiveCount] = passive;
        passiveCount++;
    }

    public float getDMG(float dmg)
    {
        return dmg * damage;

    }
}
