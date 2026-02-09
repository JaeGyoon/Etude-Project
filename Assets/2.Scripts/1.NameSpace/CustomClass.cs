using UnityEngine;
using System;
using System.Collections.Generic;


namespace EtudeProject
{
    [Serializable]
    public class AvatarStateData
    {
        public string avatarID;
        public bool unlocked;
    }

    [Serializable]
    public class PlayerSaveData
    {
        public string currentAvatarID;
        public List<AvatarStateData> avatarStateDataList;
    }

}
