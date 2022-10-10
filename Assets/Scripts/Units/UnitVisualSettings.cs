using NotFound;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "UnitVisualSettings", menuName = "NotFound/Units/VisualSettings", order = 0)]
    public class UnitVisualSettings : ScriptableObject
    {
        public TeamType TeamType;
        public int UnitLevel;
        
        public Sprite[] Walk;
        public int WalkFPS;
        public Sprite[] Attack;
        public int AttackFPS;
        public Sprite[] Idle;
        public int IdleFPS;
    }
}