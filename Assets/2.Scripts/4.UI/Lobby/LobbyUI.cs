using EtudeProject;
using UnityEngine;

public class LobbyUI : BasePopupUI
{
    [Header("Stage Select")]
    //public Transform content;
    public GameObject stageBtnPrefab;


    //[Header("Game Start")]
    //[SerializeField] PreviewSpawner spawner;

    private void Start()
    {
        StageManager.Instance.OnStageEventAction += GeneratorStageBtn;
        StageManager.Instance.ActionInvoke();

        
        
    }


    public async void OnClickGameStartBtn()
    {
        if (HeroManager.Instance.currentHero != null)
        {
            Destroy(HeroManager.Instance.currentHero);
            HeroManager.Instance.currentHero = null;
        }

        UIManager.Instance.CloseUI(UIType.LobbyUI);

        await SceneLoadManager.Instance.LoadScene("3.GameScene");
    }

    public void GeneratorStageBtn(StageCatalogSO catalog)
    {
        Transform content = UIManager.Instance.stageContent;

        foreach ( StageSO stage in catalog.stageList)
        {
            GameObject go = Instantiate(stageBtnPrefab, content);

            StageBtn stageBtn = go.GetComponent<StageBtn>();
            stageBtn.SetStage(stage);

        }
    }

    public void OnClickStageBtn()
    {
        UIManager.Instance.OpenUI(EtudeProject.UIType.StageSelect);
    }

}
