using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageSO", menuName = "Scriptable Objects/StageSO")]
public class StageSO : ScriptableObject
{
    public int stageNumber;
    public List<BiomeSO> biomeList;
}
