using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SODataManager : Manager<SODataManager>
{
    public StageCatalogSO stageCatalog;
    public HeroCatalogSO heroCatalog;

    protected override void Awake()
    {
        base.Awake();
    }

    public IEnumerator CatalogLoad()
    {        
        var stageHandle = Addressables.LoadAssetAsync<StageCatalogSO>("StageCatalogSO");
        var heroHandle = Addressables.LoadAssetAsync<HeroCatalogSO>("HeroCatalogSO");
                  

        while ( !(stageHandle.IsDone) || !(heroHandle.IsDone))
        {            
            float progress = (stageHandle.PercentComplete + heroHandle.PercentComplete) / 2f;
            Debug.Log($"<color=orange> SO 데이터를 불러오는 중... 작업 진행률:{progress}% </color>");

            yield return null;
        }

        yield return stageHandle;
        yield return heroHandle;

        stageCatalog = stageHandle.Result;
        heroCatalog = heroHandle.Result;

        Debug.Log($"<color=yellow> SO 카탈로그 전체 로드 완료! </color>");

        
    }
}
