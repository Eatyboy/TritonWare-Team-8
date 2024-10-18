using UnityEngine;
using UnityEngine.UI;

public abstract class IPerk : ScriptableObject 
{
    public string perkName;
    public abstract void Apply(Player aPlayer);
}
