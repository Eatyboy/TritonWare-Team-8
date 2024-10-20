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
    private IAbility active1;
    private IAbility active2;
    private float active1CDTimer;
    private float active2CDTimer;

    // Passive Abilities
    private IAbility passive1;
    private IAbility passive2;
    private IAbility passive3;
    private float passive1CDTimer;
    private float passive2CDTimer;
    private float passive3CDTimer;

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

        active1 = GetComponent<Ability1>();
        active2 = GetComponent<BombAbility>();
        passive1 = GetComponent<OrbitManager>();
        passive2 = GetComponent<ChainLightning>(); ;
        passive3 = null;

        active1CDTimer = 1;
        active2CDTimer = 1;
        passive1CDTimer = 1;
        passive2CDTimer = 1;
        passive3CDTimer = 1;
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

        passive1CDTimer -= dt;
        passive2CDTimer -= dt;

        if (passive1 != null && passive1CDTimer <= 0)
        {
            passive1.Activate();
            passive1CDTimer = passive1.GetCooldown();
            //if (active2CDTimer < 0)
            //{
            //    passive1CDTimer = passive1.GetCooldown();
            //}
        }
        if (passive2 != null && passive2CDTimer <= 0)
        {
            passive2.Activate();
            passive2CDTimer = passive2.GetCooldown();
            //if (active2CDTimer < 0)
            //{
            //    passive1CDTimer = passive1.GetCooldown();
            //}
        }

        Vector2 currentMovement = ctrl.Gameplay.Move.ReadValue<Vector2>();
        if (currentMovement != Vector2.zero) lastMovementDirection = currentMovement.normalized;


    }

    private void FixedUpdate() {
        float fdt = Time.fixedDeltaTime;
        Vector2 movement = ctrl.Gameplay.Move.ReadValue<Vector2>();

        rb.MovePosition(rb.position + moveSpeed * fdt * movement);
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
        // TODO
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
}
