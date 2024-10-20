using UnityEngine;

public abstract class IPerk : ScriptableObject 
{
    public bool picked;
    public PerkType perkType;
    public string perkName;
    public Sprite sprite;
    public abstract void Apply(Player aPlayer);
}
