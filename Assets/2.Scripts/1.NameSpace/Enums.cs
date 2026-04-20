using UnityEngine;

namespace EtudeProject
{
    public enum CharacterStateType
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    public enum UIType
    {
       None = 0,
       StageSelect = 1,
       HeroSelect = 2,
       Option = 3,
       QuestList = 4,
    }
}
