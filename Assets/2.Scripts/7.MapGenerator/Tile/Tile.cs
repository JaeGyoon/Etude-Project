using EtudeProject;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public Vector2Int currentCrood = new Vector2Int();

    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public MeshRenderer meshRenderer;

    private int[,] grid;
    private MapConfig config;

    public int smoothCnt = 2;

    List<Vector3> vertices = new List<Vector3>();   // 점들?
    List<int> triangles = new List<int>();          // 삼각형 연결 정보
    int currentIndex = 0;                           // 현재 정점 인덱스



    public void TileRefrash(Vector2Int crood, StageSO so)
    {        
        // 그리드 설정
        GridSetting(so);

        RandomFillMap();

        SmoothMap();

        DrawMap(so);


        /*// 랜덤 그리드 생성
        for (int x = 0; x < config.chunkResolution; x++)
        {
            for (int z = 0; z < config.chunkResolution; z++)
            {
                int diceNumber = Random.Range(0, 101);

                grid[x, z] = (diceNumber <= config.randomWallFillPercent) ? 1 : 0;




                *//*if (grid[x,z] == 1)
                {
                    //Vector3 pos = new Vector3(((-config.chunkResolution / 2 + x) + 0.5f) * 4f + this.transform.position.x, 0, ((-config.chunkResolution / 2 + z) + 0.5f) * 4f + this.transform.position.z);

                    Vector3 pos = new Vector3( (x - (20f / config.cellSize))  * config.cellSize , 0, (z - (20f / config.cellSize)) * config.cellSize );


                    AddCube(vertices, triangles, pos, config, ref currentIndex);

                    // 스무스로 덩어리지게 만들어야 함.

                    // 큐브 생성 테스트
                    //Instantiate(so.environmentPrefabs[0], pos, Quaternion.identity, this.transform);
                    //Instantiate(so.environmentPrefabs[0], pos, Quaternion.identity);
                }*//*
            }
        }

        // 그리드 스무스




        // Mesh 생성
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        

        // 자동으로 표면 방향 계산 (빛 처리용)
        mesh.RecalculateNormals();

        // Mesh 적용
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh; // 충돌도 동일 Mesh 사용
        meshRenderer.material = so.wallMaterial;*/

    }

    private void GridSetting(StageSO so)
    {
        config = so.mapConfig;

        grid = new int[config.chunkResolution, config.chunkResolution];

        vertices.Clear();
        triangles.Clear();
        currentIndex = 0;
    }

    private void RandomFillMap()
    {
        // 랜덤 그리드 생성
        for (int x = 0; x < config.chunkResolution; x++)
        {
            for (int z = 0; z < config.chunkResolution; z++)
            {
                int diceNumber = Random.Range(0, 101);

                grid[x, z] = (diceNumber <= config.randomWallFillPercent) ? 1 : 0;
            }
        }
    }

    private void SmoothMap()
    {
        int[,] newGrid = new int[config.chunkResolution, config.chunkResolution];

        for (int x = 0; x < config.chunkResolution; x++)
        {
            for (int y = 0; y < config.chunkResolution; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                {
                    newGrid[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    newGrid[x, y] = 0;
                }
                else
                {
                    newGrid[x, y] = grid[x, y];
                }
            }
        }

        grid = newGrid;
    }

    // 주변 벽 개수 검사
    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;

        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 &&
                    neighbourX < config.chunkResolution &&
                    neighbourY >= 0 &&
                    neighbourY < config.chunkResolution)
                {
                    // 자기 자신 제외
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += grid[neighbourX, neighbourY];
                    }
                }
                else
                {
                    // 맵 밖은 벽 취급
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void DrawMap(StageSO so)
    {
        for (int x = 0; x < config.chunkResolution; x++)
        {
            for (int z = 0; z < config.chunkResolution; z++)
            {
                if (grid[x, z] == 0)
                {
                    //Vector3 pos = new Vector3(((-config.chunkResolution / 2 + x) + 0.5f) * 4f + this.transform.position.x, 0, ((-config.chunkResolution / 2 + z) + 0.5f) * 4f + this.transform.position.z);

                    Vector3 pos = new Vector3((x - (20f / config.cellSize)) * config.cellSize, 0, (z - (20f / config.cellSize)) * config.cellSize);

                    AddCube(vertices, triangles, pos, config, ref currentIndex);

                    // 큐브 생성 테스트
                    //Instantiate(so.environmentPrefabs[0], pos, Quaternion.identity, this.transform);
                    //Instantiate(so.environmentPrefabs[0], pos, Quaternion.identity);
                }
            }
        }

        // Mesh 생성
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();



        // 자동으로 표면 방향 계산 (빛 처리용)
        mesh.RecalculateNormals();

        // Mesh 적용
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh; // 충돌도 동일 Mesh 사용
        meshRenderer.material = so.wallMaterial;

    }

    private void AddCube(List<Vector3> vertices, List<int> triangles, Vector3 pos, MapConfig config, ref int currentIndex)
    {
        float h = config.wallHeight;
        float s = config.cellSize;

        Vector3[] verts =
        {
            pos + new Vector3(0,0,0),
            pos + new Vector3(s,0,0),
            pos + new Vector3(s,h,0),
            pos + new Vector3(0,h,0),

            pos + new Vector3(0,0,s),
            pos + new Vector3(s,0,s),
            pos + new Vector3(s,h,s),
            pos + new Vector3(0,h,s)
        };

        // 삼각형 연결 (6면)
        int[] tris =
        {
            0,2,1, 0,3,2,
            1,2,6, 1,6,5,
            5,6,7, 5,7,4,
            4,7,3, 4,3,0,
            3,7,6, 3,6,2,
            4,0,1, 4,1,5
        };

        // 정점 추가
        for (int i = 0; i < 8; i++)
        {
            vertices.Add(verts[i]);
        }
            

        // 삼각형 추가 (인덱스 보정)
        for (int i = 0; i < tris.Length; i++)
        {
            triangles.Add(currentIndex + tris[i]);
        }


        currentIndex += 8; // 다음 큐브를 위해 증가
    }    

   

}
