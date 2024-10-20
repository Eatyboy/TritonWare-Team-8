using UnityEngine;

[CreateAssetMenu(fileName = "HealthPerk", menuName = "Perk/HealthPerk")]
public class HealthPerk : IPerk 
{
    public int healthToAdd;
    public override void Apply(Player aPlayer)
    {
        // Logic to add player's health 
        Debug.Log("Apply Health Perk");
        aPlayer.maxHealth += healthToAdd;
        aPlayer.currentHealth += healthToAdd;
        aPlayer.anim.Play(PlayerAnimations.HEALTHUP_ANIM);
    }
}
