using NotFound;
using UnityEngine;

namespace Units
{
    public class VisualController : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] private VisualState _state;
        private UnitVisualSettings _settings;
        private Sprite[] _sprites;
        private int _spriteIndex => Mathf.RoundToInt(_frameCounter);
        [SerializeField] private int _fps;
        [SerializeField] private float _frameCounter;

        public void Initialize(TeamType teamType, int unitLevel)
        {
            _settings = VisualManager.Instance.GetUnitVisualSettings(teamType, unitLevel);
        }

        public void SetState(VisualState state)
        {
            if (_state == state)
            {
                return;
            }
            _state = state;
            _frameCounter = 0;
            switch (_state)
            {
                case VisualState.Idle:
                    _sprites = _settings.Idle;
                    _fps = _settings.IdleFPS;
                    break;
                case VisualState.Attacking:
                    _sprites = _settings.Attack;
                    _fps = _settings.AttackFPS;
                    break;
                case VisualState.Walking:
                    _sprites = _settings.Walk;
                    _fps = _settings.WalkFPS;
                    break;
            }
        }

        void Update()
        {
            if (_sprites == null)
            {
                return;
            }
            
            _frameCounter += Time.deltaTime * _fps;

            if (_spriteIndex >= _sprites.Length)
            {
                _frameCounter -= _sprites.Length;
            }

            _renderer.sprite = _sprites[_spriteIndex];
        }

    }
}