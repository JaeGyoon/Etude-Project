using UnityEngine;
using System.Collections;
using EtudeProject;

public class GameManager : Manager<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(GameSequence());
    }

    IEnumerator GameSequence()
    {
        Debug.Log($"게임 시퀀스1: 팝업창 등록");
        UIManager.Instance.PopupRegist();

        Debug.Log($"게임 시퀀스2: 로딩창 열기");
        UIManager.Instance.OpenUI(UIType.LoadingUI);

        Debug.Log($"게임 시퀀스3: 플레이어 데이터 로드");
        PlayerDataManager.Instance.PlayerDataLoad();

        Debug.Log($"게임 시퀀스4: SO 데이터 로드");
        yield return SODataManager.Instance.CatalogLoad();

        Debug.Log($"게임 시퀀스5: 플레이어 데이터 복원");
        PlayerDataManager.Instance.PlayerDataApply();

        Debug.Log($"게임 시퀀스6: 로비로 이동");
        yield return SceneLoadManager.Instance.LoadScene("2.LobbyScene");

        Debug.Log($"게임 시퀀스7: 스테이지 적용");
        UIManager.Instance.StageApply();

        Debug.Log($"게임 시퀀스8: 로딩창 닫기");
        UIManager.Instance.CloseUI(UIType.LoadingUI);

        
    }

    
}
