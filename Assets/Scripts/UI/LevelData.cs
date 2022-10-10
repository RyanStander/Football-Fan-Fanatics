using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string SceneName;
        public string LevelDisplayName;
        public Sprite LevelPreviewSprite;
        [TextArea]
        public string LevelDescription;
    }
}
