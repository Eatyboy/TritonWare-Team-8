using UnityEngine;

[CreateAssetMenu(fileName = "StrengthPerk", menuName = "Perk/StrengthPerk")]
public class StrengthPerk : IPerk 
{
    public float strengthToAdd;
    public override void Apply(Player aPlayer)
    {
        // Logic to add player's strength
        Debug.Log("Apply Strength Perk");
        aPlayer.damage += strengthToAdd;
        aPlayer.anim.Play(PlayerAnimations.DAMAGEUP_ANIM);
    }
}
