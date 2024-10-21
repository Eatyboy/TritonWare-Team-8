using UnityEngine;

[CreateAssetMenu(fileName = "BombAbilityPerk", menuName = "Perk/Ability/BombAbilityPerk")]
public class BombAbilityPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.AddPassive(aPlayer.GetComponent<BombAbility>());
	}
}
