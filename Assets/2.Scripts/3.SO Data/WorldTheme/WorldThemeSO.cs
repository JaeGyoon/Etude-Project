using UnityEngine;

[CreateAssetMenu(fileName = "BiomeSO", menuName = "Scriptable Objects/Biome")]
public class BiomeSO : ScriptableObject
{
    public string biomeName;

    public GameObject groundPrefab;

    public GameObject[] enemyPrefabs;



    public GameObject[] environmentPrefabs;
}
