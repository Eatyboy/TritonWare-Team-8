using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SpeedPerk", menuName = "Perk/SpeedPerk")]
public class SpeedPerk : IPerk 
{
    public float speedToAdd;
    public override void Apply(Player aPlayer)
    {
        // Logic to add player's speed 
        Debug.Log("Apply Speed Perk");
        aPlayer.moveSpeed += speedToAdd;
        aPlayer.anim.Play(PlayerAnimations.SPEEDUP_ANIM);
    }
}
