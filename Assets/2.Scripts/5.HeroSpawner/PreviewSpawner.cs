using EtudeProject;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PreviewSpawner : MonoBehaviour
{
    //public HeroCatalogSO heroCatalog;

    //public GameObject currentHero;

    protected Transform spawnPoint;

    protected virtual async void Start()
    {        
        spawnPoint = this.transform;
        await SpawnHero();

        //UIManager.Instance.spawner = this;

        UIManager.Instance.OpenUI(UIType.LobbyUI);
    }

    public async Task<GameObject> SpawnHero()
    {
        if (HeroManager.Instance.currentHero != null)
        {
            Addressables.ReleaseInstance(HeroManager.Instance.currentHero);
            HeroManager.Instance.currentHero = null;
        }



        string heroName = PlayerDataManager.Instance.currentSaveData.currentHeroID;



        Debug.Log($"히어로 네임 {heroName}");

        HeroSO so = SODataManager.Instance.heroCatalog.GetHero(heroName);

        Debug.Log($"어드레스 키 {so.addressKey}");

        var handle = Addressables.InstantiateAsync(so.addressKey, spawnPoint.position, spawnPoint.rotation);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            HeroManager.Instance.currentHero = handle.Result;
            return HeroManager.Instance.currentHero;
        }
        else
        {
            Debug.Log(" 어드레서블 Spawn 실패! ");
            return null;
        }
            
    }
}
