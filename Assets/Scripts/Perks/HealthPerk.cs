using UnityEngine;

[CreateAssetMenu(fileName = "HealthPerk", menuName = "Perk/HealthPerk")]
public class HealthPerk : IPerk 
{
    public float healthToAdd;
    public override void Apply(Player aPlayer)
    {
        // Logic to add player's health 
        Debug.Log("Apply Health Perk");
    }
}