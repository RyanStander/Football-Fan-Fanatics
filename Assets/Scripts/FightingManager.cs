using System;
using System.Collections.Generic;
using NotFound.Utils;
using Units;

namespace NotFound
{
    public class FightingManager : MonoSingleton<FightingManager>
    {

        private Queue<DamageEvent> _damageEvents;

        public void RegisterDamage(Hooligan target, int damage)
        {
            _damageEvents ??= new Queue<DamageEvent>();
            _damageEvents.Enqueue(new DamageEvent(target, damage));
        }

        private void HandleDamage()
        {
            if (_damageEvents == null)
            {
                return;
            }

            while (_damageEvents.Count > 0)
            {
                DamageEvent damageEvent = _damageEvents.Dequeue();

                if (damageEvent.Target.Destroyed)
                {
                    continue;
                }
                
                damageEvent.Target.ApplyDamage(damageEvent.Damage);
            }
        }
        
        private void Update()
        {
            HandleDamage();
        }
    }
}