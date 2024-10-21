using UnityEngine;

[CreateAssetMenu(fileName = "MinionAbilityPerk", menuName = "Perk/Ability/MinionAbilityPerk")]
public class MinionAbilityPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.MinionAbility = aPlayer.GetComponent<MinionAbility>();
	}
}
