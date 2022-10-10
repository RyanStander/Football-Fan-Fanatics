using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelDataHolder")]
    public class LevelDataHolder : ScriptableObject
    {
        public List<LevelData> LevelDatas;
        public GameObject LevelDataDisplayPrefab;
    }
}
