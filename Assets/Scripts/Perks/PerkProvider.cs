using UnityEngine;
using System.Collections.Generic;

public class PerkProvider : MonoBehaviour
{
    [SerializeField] private List<IPerk> availablePerks;

    public IPerk GetRandomPerk()
    {
        IPerk thePerk = availablePerks[Random.Range(0, availablePerks.Count)];
        return thePerk;
	}
}
