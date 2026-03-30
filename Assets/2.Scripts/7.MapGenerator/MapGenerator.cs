using EtudeProject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int currentStage;
    [SerializeField] private List<ThemeDatabase> themeList;
    [SerializeField] private WorldThemeSO currentTheme;

    private const int tileDistance = 40;
    private GameObject[,] tiles = new GameObject[3, 3];

    public Transform playerPos;
    public Vector2Int currentTile;

    public Vector2Int currentPos;

    void Start()
    {
        CurrentStageLoad();
        ThemeRandomSelect();

        GroundGeneration();
    }

    private void Update()
    {
        if (playerPos == null)
        {
            Debug.Log("플레이어 포스 없음");
            return;
        }

        PlayerTracking();
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
                Vector3 pos = new Vector3(x * tileDistance, 0, z * tileDistance);

                Quaternion planeQuaternion = Quaternion.Euler(new Vector3(0, -180f, 0));

                tiles[x + 1, z + 1] = Instantiate(currentTheme.groundPrefab, pos, planeQuaternion);
            }
        }

        currentTile = Vector2Int.zero;
    }

    private void PlayerTracking()
    {        
        // 중앙 타일을 (0,0) 기준으로 -1, 0 , 1 값으로 현재 있는 위치 표시
        currentPos = new Vector2Int(
            (int)(playerPos.position.x / (tileDistance / 2)),
            (int)(playerPos.position.z / (tileDistance / 2))
        );

        if (currentPos != currentTile)
        {
            Vector2Int direction = currentPos - currentTile;

            if ( direction.x != 0)
            {
                TileMovement(direction.x, Direction.Horizontal);
            }

            if (direction.y != 0)
            {
                TileMovement(direction.y, Direction.Vertical);
            }

            currentTile = currentPos;
            /*TileMovement(currentPos - currentTile);
            currentTile = currentPos;*/
        }
    }

    private void TileMovement(int direction, Direction eDir)
    {      
        for (int i = 0; i < 3; i++)
        {
            int from = direction > 0 ? 0 : 2;
            int to = direction > 0 ? 2 : 0;

            GameObject targetTile = (eDir == Direction.Horizontal) ? tiles[from, i] : tiles[i, from];

            Vector3 moveOffset = (eDir == Direction.Horizontal)
            ? new Vector3(tileDistance * 3 * direction, 0, 0)
            : new Vector3(0, 0, tileDistance * 3 * direction);

            targetTile.transform.position += moveOffset;


            for (int j = 0; j < 3 - 1; j++)
            {
                int current = direction > 0 ? j : 2 - j;
                int next = direction > 0 ? j + 1 : 1 - j;

                if (eDir == Direction.Horizontal)
                    tiles[current, i] = tiles[next, i];
                else
                    tiles[i, current] = tiles[i, next];
            }

            if (eDir == Direction.Horizontal)
                tiles[to, i] = targetTile;
            else
                tiles[i, to] = targetTile;


        }

    }
}
