using NotFound;

namespace Units
{
    public class MeleeFightController : FightControllerBase
    {
        protected override void Attack(Hooligan enemy, Hooligan owner)
        {
            FightingManager.Instance.RegisterDamage(enemy, owner.Damage);
        }
    }
}