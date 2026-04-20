using EtudeProject;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerDataManager : Manager<PlayerDataManager>
{    
    public PlayerSaveData currentSaveData;

    private string SavePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    protected override void Awake()
    {
        base.Awake();

        PlayerDataLoad();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlayerDataLoad()
    {
        Debug.Log($"경로 : {SavePath}");

        if (!File.Exists(SavePath))
        {
            CreatePlayerData();
            return;
        }

        string json = File.ReadAllText(SavePath);

        currentSaveData = JsonUtility.FromJson<PlayerSaveData>(json);

        Debug.Log("<color=green> 플레이어 데이터 로드! </color>");

        // 검증
        ValidateHeroState();

    }

    private void CreatePlayerData()
    {
        Debug.Log("<color=red> 새로운 플레이어 데이터 생성! </color>");

        HeroCatalog database = HeroManager.Instance.heroCatalog;
        List<HeroStateData> state = new List<HeroStateData>();

        foreach (HeroSO heroSO in database.heroList)
        {
            HeroStateData heroState = new HeroStateData();
            heroState.heroID = heroSO.heroName;
            heroState.unlocked = heroSO.defaultUnlocked;

            state.Add(heroState);
        }

        var firstHero = database.heroList.First(hero => hero.defaultUnlocked);

        currentSaveData = new PlayerSaveData
        {
            currentHeroID = firstHero.heroName,
            heroStateDataList = state
        };

        PlayerDataSave();
    }

    private void ValidateHeroState()
    {
        Debug.Log("<color=gray> 업데이트 내역과 비교! </color>");

        HeroCatalog database = HeroManager.Instance.heroCatalog;

        foreach (HeroSO heroSO in database.heroList)
        {
            // 현재 heroStateDataList에 없는 heroSO가 있을 경우
            if ( !currentSaveData.heroStateDataList.Any(hero => hero.heroID == heroSO.heroName))
            {
                HeroStateData heroState = new HeroStateData();
                heroState.heroID = heroSO.heroName;
                heroState.unlocked = heroSO.defaultUnlocked;

                currentSaveData.heroStateDataList.Add(heroState);
                Debug.Log("<color=orange> 신규 영웅 추가! </color>");
            }
        }
    }

    public void PlayerDataSave()
    {        
        //true => prettyPrint: 보기 좋게 줄바꿈/들여쓰기 할지 여부
        string json = JsonUtility.ToJson(currentSaveData, true);

        File.WriteAllText(SavePath, json);

        Debug.Log("저장!");
    }
}
