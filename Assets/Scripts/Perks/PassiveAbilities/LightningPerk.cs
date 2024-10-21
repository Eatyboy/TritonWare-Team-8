using UnityEngine;

[CreateAssetMenu(fileName = "LightningPerk", menuName = "Perk/Ability/LightningPerk")]
public class LightningPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.AddPassive(aPlayer.GetComponent<LightningAbility>());
	}
}
