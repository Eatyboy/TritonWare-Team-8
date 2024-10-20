using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectingShieldAbility : MonoBehaviour, IAbility
{
    public GameObject shieldPrefab;    // Prefab for the reflecting shield
    public float shieldDuration = 2f;  // Duration the shield stays active
    public float cooldown = 5f;        // Cooldown time before it can be activated again

    private Player player;
    private GameObject activeShield;   // The currently active shield
    private bool isShieldActive = false; // To check if the shield is already active

    public float GetCooldown()
    {
        return cooldown;
    }

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Activate()
    {
        if (!isShieldActive)
        {
            StartCoroutine(ActivateShield());
        }
    }

    private IEnumerator ActivateShield()
    {
        // Spawn the shield in front of the player
        Vector3 shieldPosition = player.transform.position + (Vector3)player.GetMovementDirection().normalized * 1.5f;
        activeShield = Instantiate(shieldPrefab, shieldPosition, Quaternion.identity, player.transform);

        isShieldActive = true;

        // Rotate shield to face player's movement direction
        //if (player.GetMovementDirection() != Vector3.zero)
        //{
        //    float angle = Mathf.Atan2(player.GetMovementDirection().y, player.GetMovementDirection().x) * Mathf.Rad2Deg;
        //    activeShield.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        //}

        yield return new WaitForSeconds(shieldDuration);

        // Deactivate the shield after the duration ends
        Destroy(activeShield);
        isShieldActive = false;
    }
}
