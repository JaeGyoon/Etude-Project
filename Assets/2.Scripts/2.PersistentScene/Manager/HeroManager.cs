using System.Linq;
using UnityEngine;

public class HeroManager : Manager<HeroManager>
{
    public HeroCatalog heroCatalog;

    private GameObject currentHero;
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public bool HeroUnlockCheck(string heroName)
    {
        return PlayerDataManager.Instance.currentSaveData.heroStateDataList.Any(hero => (hero.heroID == heroName) && hero.unlocked);
    }

    public void SelectHero(string heroName)
    {
        if (HeroUnlockCheck(heroName))
        {
            Debug.Log("잠긴 캐릭터 입니다.");
            return;
        }

        PlayerDataManager.Instance.currentSaveData.currentHeroID = heroName;
        PlayerDataManager.Instance.PlayerDataSave();

        // 생성은 추후 오브젝트 풀을 통해 구현할 예정


    }
}
