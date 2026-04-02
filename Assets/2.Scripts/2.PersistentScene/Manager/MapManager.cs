using UnityEngine;

public class MapManager : Manager<MapManager>
{
    public BiomeSO currentBiome;
    public StageSO currentStage;

    protected override void Awake()
    {
        base.Awake();
    }

    public void StageApply(StageSO stage)
    {
        currentStage = stage;
        BiomeApply(stage);
    }

    private void BiomeApply(StageSO stage)
    {
        int index = Random.Range(0, stage.biomeList.Count);
        currentBiome = stage.biomeList[index];

        Debug.Log($"선택된 바이옴 : {currentBiome.biomeName}");
    }
}
