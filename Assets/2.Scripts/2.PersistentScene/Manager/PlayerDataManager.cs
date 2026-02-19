using EtudeProject;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerDataManager : Manager<PlayerDataManager>
{
    // 프로퍼티를 사용해 대문자 사용
    //public PlayerSaveData CurrentSaveData { get; private set; }
    public PlayerSaveData CurrentSaveData;

    private string SavePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerDataLoad();
    }

    public void PlayerDataLoad()
    {
        if (!File.Exists(SavePath))
        {
            CreatePlayerData();
            return;
        }

        string json = File.ReadAllText(SavePath);

        CurrentSaveData = JsonUtility.FromJson<PlayerSaveData>(json);

        Debug.Log("<color=green> 플레이어 데이터 로드! </color>");

        // 검증
        ValidateHeroState();

    }

    private void CreatePlayerData()
    {
        Debug.Log("<color=red> 새로운 플레이어 데이터 생성! </color>");

        HeroDatabase database = HeroManager.Instance.heroDatabase;
        List<HeroStateData> state = new List<HeroStateData>();

        foreach (HeroSO heroSO in database.heroList)
        {
            HeroStateData heroState = new HeroStateData();
            heroState.heroID = heroSO.heroName;
            heroState.unlocked = heroSO.defaultUnlocked;

            state.Add(heroState);
        }

        var firstHero = database.heroList.First(hero => hero.defaultUnlocked);

        CurrentSaveData = new PlayerSaveData
        {
            currentHeroID = firstHero.heroName,
            heroStateDataList = state
        };

        PlayerDataSave();
    }

    private void ValidateHeroState()
    {
        Debug.Log("<color=gray> 업데이트 내역과 비교! </color>");

        HeroDatabase database = HeroManager.Instance.heroDatabase;

        foreach (HeroSO heroSO in database.heroList)
        {
            // 현재 heroStateDataList에 없는 heroSO가 있을 경우
            if ( !CurrentSaveData.heroStateDataList.Any(hero => hero.heroID == heroSO.heroName))
            {
                HeroStateData heroState = new HeroStateData();
                heroState.heroID = heroSO.heroName;
                heroState.unlocked = heroSO.defaultUnlocked;

                CurrentSaveData.heroStateDataList.Add(heroState);
                Debug.Log("<color=orange> 신규 영웅 추가! </color>");
            }
        }
    }

    public void PlayerDataSave()
    {
        //true => prettyPrint: 보기 좋게 줄바꿈/들여쓰기 할지 여부
        string json = JsonUtility.ToJson(CurrentSaveData, true);

        File.WriteAllText(SavePath, json);
    }
}
