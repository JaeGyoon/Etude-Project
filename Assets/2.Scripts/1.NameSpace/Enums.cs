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
       LobbyUI = 2,
       LoadingUI = 3,
    }

    public enum Direction
    {
        Horizontal = 0,
        Vertical = 1,

    }
}
