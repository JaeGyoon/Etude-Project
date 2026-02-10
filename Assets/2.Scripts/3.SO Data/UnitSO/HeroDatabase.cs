using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroDatabase", menuName = "Scriptable Objects/HeroDatabase")]
public class HeroDatabase : ScriptableObject
{
    public List<HeroSO> heroList;

    public HeroSO GetHero(string heroName)
    {
        return heroList.Find( heroSO => heroSO.heroName == heroName );
    }
}
