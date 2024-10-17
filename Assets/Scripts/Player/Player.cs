using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActions ctrl;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float dmgMult;
    [SerializeField] public int health;

    [SerializeField] private float invulnerabilityDuration = 1.5f; // Duration of invulnerability
    private float transparencyFlashSpeed = 0.2f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Awake()
    {
        ctrl = new InputActions();
    }

    void Start()
    {
        // Freeze rotation on Z-axis
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        ctrl.Enable();
        ctrl.Gameplay.Ability1.performed += Ability1;
        ctrl.Gameplay.Ability2.performed += Ability2;
    }

    private void OnDisable()
    {
        ctrl.Gameplay.Ability1.performed -= Ability1;
        ctrl.Gameplay.Ability2.performed -= Ability2;
        ctrl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;
        Vector2 movement = ctrl.Gameplay.Move.ReadValue<Vector2>();

        // Move using physics
        Vector2 newPosition = rb.position + moveSpeed * fdt * movement;
        rb.MovePosition(newPosition);

        // Keep rotation locked at 0
        //transform.rotation = Quaternion.identity;
    }

    private void Ability1(InputAction.CallbackContext ctx)
    {
        // Implement ability logic here
    }

    private void Ability2(InputAction.CallbackContext ctx)
    {
        // Implement ability logic here
    }

    // XP and Leveling System
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
        moveSpeed += 1f;
        // Upgrade other stats here if needed (health, damage, etc.)
    }

    // Take damage when hit by the player's projectile
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
            health -= damageAmount;

            if (health <= 0)
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
        throw new NotImplementedException();
    }
}
