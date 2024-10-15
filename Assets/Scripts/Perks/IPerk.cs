using UnityEngine;
using UnityEngine.UI;

public abstract class IPerk : ScriptableObject 
{
    public string perkName;
    public Image perkImage;
    public abstract void Apply(GameObject aGameObject);
}
