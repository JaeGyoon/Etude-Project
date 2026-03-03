using UnityEngine;

[CreateAssetMenu(fileName = "HeroSO", menuName = "Scriptable Objects/HeroSO")]
public class HeroSO : ScriptableObject
{
    public string heroName;
    public Sprite heroImage;
    public string addressKey;
    public bool defaultUnlocked;
}
