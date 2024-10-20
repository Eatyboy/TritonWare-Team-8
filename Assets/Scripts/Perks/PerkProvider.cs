using UnityEngine;
using System.Collections.Generic;

public enum PerkType
{
    SpeedUp,
    StrengthUp,
    HealthUp,

    EggBomb,
    InternalBurst,
    Lightning,
    Orbit
}

public class PerkProvider : MonoBehaviour
{
    [SerializeField] private List<IPerk> availablePerksList;
    static public List<int> pickedPerkIdx = new List<int>();

    private void Start()
    {
        foreach(IPerk aPerk in availablePerksList)
        {
            aPerk.picked = false;
        }
    }

    public IPerk GetRandomPerk()
    {
        // remove the picked ability first
        foreach(IPerk aPerk in availablePerksList)
        { 
		    if (aPerk.perkType >= PerkType.EggBomb && aPerk.picked)
            {
                availablePerksList.Remove(aPerk);
            }
        }

        int idx = Random.Range(0, availablePerksList.Count-1);
        // don't want to have same one
        while (pickedPerkIdx.Contains(idx))
        {
            idx++;
            idx %= availablePerksList.Count;
        }

        pickedPerkIdx.Add(idx);
        IPerk thePerk = availablePerksList[idx];
        if (pickedPerkIdx.Count == 3)
        {
            Debug.Log("Clear idx");
            pickedPerkIdx.Clear();
		}

        return thePerk;
	}
}
