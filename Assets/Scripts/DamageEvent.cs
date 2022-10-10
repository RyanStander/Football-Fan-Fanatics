using Units;

namespace NotFound
{
    public struct DamageEvent
    {
        public readonly Hooligan Target;
        public readonly int Damage;

        public DamageEvent(Hooligan target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}