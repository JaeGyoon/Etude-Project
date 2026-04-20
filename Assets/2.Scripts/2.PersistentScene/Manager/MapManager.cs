using UnityEngine;

public class MapManager : Manager<MapManager>
{
    public StageSO currentBiome;
    public StageCatalogSO currentStage;

    protected override void Awake()
    {
        base.Awake();
    }

}
