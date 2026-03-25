using UnityEngine;

[CreateAssetMenu(fileName = "WorldThemeSO", menuName = "Scriptable Objects/WorldTheme")]
public class WorldThemeSO : ScriptableObject
{
    public string themeName;

    public Material groundMaterial;

    public GameObject[] enemyPrefabs;
    public GameObject[] props;

    public string[] stageScenes;
}
