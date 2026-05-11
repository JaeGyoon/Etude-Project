using EtudeProject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private StageSO currentStage;
    [SerializeField] private MapConfig config;

    //private const int tileDistance = 40;
    private GameObject[,] tiles;

    public Transform playerPos;
    public Vector2Int currentTile;
    public Vector2Int currentPos;



    void Start()
    {
        CurrentStageLoad();        

        GroundGenerate();
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
        currentStage = StageManager.Instance.currentStageSO;
        config = currentStage.mapConfig;
        tiles = new GameObject[config.gridCount, config.gridCount];

    }


    private void GroundGenerate()
    {
        for (int x = -config.halfGrid; x <= config.halfGrid; x++)
        {
            for (int z = -config.halfGrid; z <= config.halfGrid; z++)
            {
                Vector3 pos = new Vector3(x * config.tileSize, 0, z * config.tileSize);

                Quaternion planeQuaternion = Quaternion.Euler(new Vector3(0, -180f, 0));

                GameObject tile = Instantiate(currentStage.groundPrefab, pos, planeQuaternion);

                int arrayX = x + config.halfGrid;
                int arrayZ = z + config.halfGrid;
                Vector2Int tileCoord = new Vector2Int(arrayX, arrayZ);


                tiles[arrayX, arrayZ] = tile;

                TileRefrash(tile, tileCoord);
            }
        }

        currentTile = Vector2Int.zero;
    }

    private void PlayerTracking()
    {
        // 중앙 타일을 (0,0) 기준으로 -1, 0 , 1 값으로 현재 있는 위치 표시
        currentPos = new Vector2Int(
            (int)(playerPos.position.x / (config.tileSize )),
            (int)(playerPos.position.z / (config.tileSize ))
        );

        if (currentPos != currentTile)
        {
            Vector2Int direction = currentPos - currentTile;

            if (direction.x != 0)
            {
                TileMovement(direction.x, Direction.Horizontal);
            }

            if (direction.y != 0)
            {
                TileMovement(direction.y, Direction.Vertical);
            }

            currentTile = currentPos;
            

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
            ? new Vector3(config.tileSize * 3 * direction, 0, 0)
            : new Vector3(0, 0, config.tileSize * 3 * direction);

            targetTile.transform.position += moveOffset;

            Vector2Int coord = GetChunkCoord(targetTile.transform.position);

            TileRefrash(targetTile, coord);

            for (int j = 0; j < 3 - 1; j++)
            {
                int current = direction > 0 ? j : 2 - j;
                int next = direction > 0 ? j + 1 : 1 - j;

                if (eDir == Direction.Horizontal)
                {
                    tiles[current, i] = tiles[next, i];
                }                    
                else
                {
                    tiles[i, current] = tiles[i, next];
                }                    
            }

            if (eDir == Direction.Horizontal)
            {
                tiles[to, i] = targetTile;

            }
            else
            {
                tiles[i, to] = targetTile;
            }

        }



    }


    private void TileRefrash(GameObject go, Vector2Int tileCoord)
    {
        // 기존 맵 제거
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        Debug.Log("타일 새로고침!");

        Tile tile = go.GetComponent<Tile>();

        tile.currentCrood = tileCoord;

        tile.TileRefrash(tileCoord, currentStage);



    }

    // 월드 좌표 → 타일 좌표 변환
    private Vector2Int GetChunkCoord(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(pos.x / config.tileSize),
            Mathf.RoundToInt(pos.z / config.tileSize)
        );
    }
}
