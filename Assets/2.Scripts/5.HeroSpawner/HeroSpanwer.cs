using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HeroSpanwer : MonoBehaviour
{

    public HeroDatabase heroDatabase;
    private GameObject currentHero;
    private Transform spawnPoint;


    protected virtual void Start()
    {
        spawnPoint = this.transform;
        SpawnHero();
    }

    public async void SpawnHero()
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

        var handle = Addressables.InstantiateAsync(so.addressKey, spawnPoint.position, Quaternion.Euler(0, 180, 0));

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            currentHero = handle.Result;            
        }

    }
}
