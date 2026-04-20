using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroCatalog", menuName = "Scriptable Objects/HeroCatalog")]
public class HeroCatalog : ScriptableObject
{
    public List<HeroSO> heroList;

    Dictionary<string,HeroSO> heroDict;

    private void OnEnable()
    {
        heroDict = new Dictionary<string,HeroSO>();

        foreach(HeroSO hero in heroList)
        {
            heroDict[hero.heroName] = hero;
        }
    }


    public HeroSO GetHero(string heroName)
    {
        if ( heroDict.TryGetValue(heroName, out HeroSO so))
        {
            return so;
        }

        Debug.Log($"<color=red> HeroDatabase에 {heroName} 없음!  </color>");
        return null;
    }
}
