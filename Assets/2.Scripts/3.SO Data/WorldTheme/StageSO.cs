using UnityEngine;
using EtudeProject;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Objects/Stage")]
public class StageSO : ScriptableObject
{
    public int stageNumber;
    public string stageName;

    public GameObject groundPrefab;
    public GameObject[] enemyPrefabs;
    public GameObject[] environmentPrefabs;

    public Sprite stageImage;

    public MapConfig mapConfig;

    public Material wallMaterial;
}
