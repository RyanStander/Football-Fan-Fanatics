using UnityEngine;

namespace NotFound
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TeamData")]
    public class TeamData : ScriptableObject
    {
        public GameObject TeamUnitPrefab;
        public Sprite TeamBaseSprite;
    }
}
