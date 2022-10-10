using System;
using NotFound;
using Audio;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class Hooligan : MonoBehaviour
    {

        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private VisualController _visualController;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private UnitSoundEffects _fightSfx;

        public TargetController TargetController;
        
        public bool Destroyed { get; private set; } = false;

        public TeamType TeamType => _teamType;
        public int TeamIndex => _teamIndex;
        public int UpgradeLevel => _upgradeLevel;
        public int Health => _health;
        public int Damage => _damage;
        public float AttackSpeed => _attackSpeed;

        private int _teamIndex;
        private int _upgradeLevel = 1;
        private TeamType _teamType;

        public void SetTeam(int teamIndex, TeamType teamType, int upgradeLevel)
        {
            _teamIndex = teamIndex;
            _upgradeLevel = upgradeLevel;
            if (_teamIndex == 0)
            {
                gameObject.AddComponent<SelectableUnitComponent>();
            }

            if (_visualController != null)
            {
                _visualController.Initialize(teamType, upgradeLevel);
                _visualController.SetState(VisualState.Walking);
            }
        }

        public void ApplyDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            _fightSfx.SpawnAudioSFX();
            if (_health <= 0)
            {
                DestroyUnit();
            }
        }

        public void DestroyUnit()
        {
            Destroyed = true;
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_agent.velocity.magnitude > 0.1f)
            {
                _visualController.SetState(VisualState.Walking);
            }
            else
            {
                _visualController.SetState(VisualState.Idle);
            }
        }
    }
}