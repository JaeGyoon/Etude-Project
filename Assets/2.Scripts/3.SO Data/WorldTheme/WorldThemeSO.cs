using UnityEngine;

[CreateAssetMenu(fileName = "WorldThemeSO", menuName = "Scriptable Objects/WorldTheme")]
public class WorldThemeSO : ScriptableObject
{
    public string themeName;

    public GameObject groundPrefab;

    public GameObject[] enemyPrefabs;
    public GameObject[] props;

    public string[] stageScenes;
}
