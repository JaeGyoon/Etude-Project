using System.Collections.Generic;
using UnityEngine;
using EtudeProject;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int currentStage;
    [SerializeField] private List<ThemeDatabase> themeList;
    [SerializeField] private WorldThemeSO currentTheme;

    private const int tileSize = 20;
    private GameObject[,] tiles = new GameObject[3, 3];

    public Vector2Int playerPos;
    void Start()
    {
        CurrentStageLoad();
        ThemeRandomSelect();

        GroundGeneration();
    }

    private void CurrentStageLoad()
    {
        currentStage = PlayerDataManager.Instance.currentSaveData.currentStage;
    }

    private void ThemeRandomSelect()
    {
        ThemeDatabase database = themeList[currentStage];

        int index = Random.Range(0, database.themes.Count);

        currentTheme = database.themes[index];

        Debug.Log($"선택된 테마: {currentTheme.themeName}");
    }

    private void GroundGeneration()
    {


        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);

                Quaternion planeQuaternion = Quaternion.Euler(new Vector3(0, -180f, 0));

                tiles[x + 1, z + 1] = Instantiate(currentTheme.groundPrefab, pos, planeQuaternion);

                
            }
        }

        playerPos = Vector2Int.zero;
    }
}
