using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageCatalogSO", menuName = "Scriptable Objects/StageCatalogSO")]
public class StageCatalogSO : ScriptableObject
{    
    public List<StageSO> stageList;
}
