using UnityEngine;

[CreateAssetMenu(fileName = "LightningPerk", menuName = "Perk/Ability/LightningPerk")]
public class LightningPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.LightningAbility = aPlayer.GetComponent<LightningAbility>();
	}
}
