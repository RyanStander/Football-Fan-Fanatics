using Units;
using UnityEngine;

namespace NotFound
{


    public abstract class FightControllerBase : MonoBehaviour
    {

        [SerializeField] private Hooligan _owner;

        private float _timeSinceLastHit = 0f;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (_timeSinceLastHit < _owner.AttackSpeed)
            {
                return;
            }

            if (other == null || other.transform.parent == null || other.GetComponent<FightControllerBase>() == null)
            {
                return;
            }

            Hooligan enemy = other.transform.parent.GetComponent<Hooligan>();
            if (enemy == null)
            {
                return;
            }

            if (enemy.TeamIndex != _owner.TeamIndex)
            {
                Attack(enemy, _owner);
                _timeSinceLastHit = 0;
            }
        }

        protected abstract void Attack(Hooligan enemy, Hooligan owner);

        protected virtual void Update()
        {
            _timeSinceLastHit += Time.deltaTime;
        }
    }
}

