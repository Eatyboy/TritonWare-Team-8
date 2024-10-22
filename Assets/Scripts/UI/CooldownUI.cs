using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] Image FireTrailCooldownFront;
    [SerializeField] Image TeleportCooldownFront;
    [SerializeField] Player player;

    private void OnEnable()
    {
        EventManager.RegisterToEvent(EventManager.Events.UnlockActive1, OnUnlockActive1);
        EventManager.RegisterToEvent(EventManager.Events.UnlockActive2, OnUnlockActive2);
    }

    private void OnDisable()
    {
        EventManager.UnregisterFromEvent(EventManager.Events.UnlockActive1, OnUnlockActive1);
        EventManager.UnregisterFromEvent(EventManager.Events.UnlockActive2, OnUnlockActive2);
    }

    private void Update()
    {
        if (player.active1 != null)
        {
            ReflectingShieldAbility r = player.GetComponent<ReflectingShieldAbility>();
            FireTrailCooldownFront.fillAmount = player.active1CDTimer / r.GetCooldown();
            Debug.Log("active1CDTimer: " + player.active1CDTimer);
		}

        if (player.active2 != null)
        {
            TeleportStrike t = player.GetComponent<TeleportStrike>();
            TeleportCooldownFront.fillAmount = t.cooldownTimer / t.GetCooldown();
		}

    }

    private void OnUnlockActive1()
    {
        FireTrailCooldownFront.transform.parent.gameObject.SetActive(true);
    }

    private void OnUnlockActive2()
    {
        TeleportCooldownFront.transform.parent.gameObject.SetActive(true);
    }
}
