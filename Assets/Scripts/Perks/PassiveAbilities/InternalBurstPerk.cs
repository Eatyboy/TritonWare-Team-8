using UnityEngine;

[CreateAssetMenu(fileName = "InternalBurstPerk", menuName = "Perk/Ability/InternalBurstPerk")]
public class InternalBurstPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.AddPassive(aPlayer.GetComponent<InternalBurst>());
	}
}
