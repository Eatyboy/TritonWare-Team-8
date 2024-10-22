using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RegenPerk", menuName = "Perk/RegenPerk")]

public class RegenPerk : IPerk
{
    public float regenToAdd;
    public override void Apply(Player aPlayer)
    {
        // Logic to add player's health 
        Debug.Log("Apply Regen Perk");
        aPlayer.healthRegen += regenToAdd;
        aPlayer.anim.Play(PlayerAnimations.REGENUP_ANIM);
    }
}
