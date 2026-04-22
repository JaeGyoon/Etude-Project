using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using EtudeProject;

public class StageManager : Manager<StageManager>
{
    public StageCatalogSO catalogSO;
    public StageSO currentStageSO;

    // 스테이지 이벤트 동작 시 : 스테이지 버튼 생성 ,
    public event Action<StageCatalogSO> OnStageEventAction;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AddressableLoad();
    }

    private void AddressableLoad()
    {
        Addressables.LoadAssetAsync<StageCatalogSO>("StageCatalogSO").Completed += OnLoaded;
    }

    private void OnLoaded(AsyncOperationHandle<StageCatalogSO> handle)
    {
        if ( handle.Status == AsyncOperationStatus.Succeeded)
        {
            catalogSO = handle.Result;

            

            //OnStageEventAction?.Invoke(catalogSO);
        }

    }

    public void ActionInvoke()
    {
        OnStageEventAction?.Invoke(catalogSO);
    }

    public void SelectStage(StageSO so)
    {
        currentStageSO = so;

        UIManager.Instance.CloseUI(UIType.StageSelect);
    }

}
