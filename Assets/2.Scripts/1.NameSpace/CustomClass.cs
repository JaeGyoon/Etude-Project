using UnityEngine;
using System;
using System.Collections.Generic;


namespace EtudeProject
{
    [Serializable]
    public class HeroStateData
    {
        public string heroID;
        public bool unlocked;        
    }

    [Serializable]
    public class PlayerSaveData
    {
        public string currentHeroID;
        public List<HeroStateData> heroStateDataList;
    }

}
