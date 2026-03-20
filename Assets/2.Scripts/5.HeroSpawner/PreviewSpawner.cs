using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;

public class PreviewSpawner : MonoBehaviour
{
    public HeroDatabase heroDatabase;

    public GameObject currentHero;

    protected Transform spawnPoint;

    protected virtual async void Start()
    {
        spawnPoint = this.transform;
        await SpawnHero();
    }

    public async Task<GameObject> SpawnHero()
    {
        if (currentHero != null)
        {
            Addressables.ReleaseInstance(currentHero);
            currentHero = null;
        }

        string heroName = PlayerDataManager.Instance.CurrentSaveData.currentHeroID;

        Debug.Log($"히어로 네임 {heroName}");

        HeroSO so = heroDatabase.GetHero(heroName);

        Debug.Log($"어드레스 키 {so.addressKey}");

        var handle = Addressables.InstantiateAsync(so.addressKey, spawnPoint.position, spawnPoint.rotation);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentHero = handle.Result;
            return currentHero;
        }
        else
        {
            Debug.Log(" 어드레서블 Spawn 실패! ");
            return null;
        }
            
    }
}
