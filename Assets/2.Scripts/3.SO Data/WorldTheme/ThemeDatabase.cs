using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ThemeDatabase", menuName = "Scriptable Objects/ThemeDatabase")]
public class ThemeDatabase : ScriptableObject
{
    public List<WorldThemeSO> themes;
}
