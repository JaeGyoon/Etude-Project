using EtudeProject;
using System.IO;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    // 프로퍼티를 사용해 대문자 사용
    public PlayerSaveData CurrentSaveData { get; private set; }

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

        // 검증
        ValidateAvatarState();

    }

    private void CreatePlayerData()
    {
        Debug.Log("<color=red> 새로운 플레이어 데이터 생성! </color>");
    }

    private void ValidateAvatarState()
    {

    }

    public void PlayerDataSave()
    {
        //true => prettyPrint: 보기 좋게 줄바꿈/들여쓰기 할지 여부
        string json = JsonUtility.ToJson(CurrentSaveData, true);

        File.WriteAllText(SavePath, json);
    }
}
