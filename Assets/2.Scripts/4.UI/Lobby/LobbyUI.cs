using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [Header("Stage Select")]
    //public Transform content;
    public GameObject stageBtnPrefab;


    [Header("Game Start")]
    [SerializeField] PreviewSpawner spawner;

    private void Start()
    {
        StageManager.Instance.OnStageEventAction += GeneratorStageBtn;
        StageManager.Instance.ActionInvoke();



        UIManager.Instance.StageApply();
    }


    public async void OnClickGameStartBtn()
    {
        if (spawner.currentHero != null)
        {
            Destroy(spawner.currentHero);
            spawner.currentHero = null;
        }

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
