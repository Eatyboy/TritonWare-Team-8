using UnityEngine;

[CreateAssetMenu(fileName = "OrbitManagerPerk", menuName = "Perk/Ability/OrbitManagerPerk")]
public class OrbitManagerPerk : IPerk
{
    public override void Apply(Player aPlayer)
    {
        aPlayer.AddPassive(aPlayer.GetComponent<OrbitManager>());
	}
}
