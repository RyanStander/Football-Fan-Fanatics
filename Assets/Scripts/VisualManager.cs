using System.Collections.Generic;
using NotFound.Utils;
using Units;
using UnityEngine;

namespace NotFound
{
    public class VisualManager : MonoSingleton<VisualManager>
    {

        [SerializeField] private List<UnitVisualSettings> _unitVisualSettingsList;

        private Dictionary<TeamType, Dictionary<int, UnitVisualSettings>> _visuals;

        protected override void Awake()
        {
            base.Awake();
            _visuals = new Dictionary<TeamType, Dictionary<int, UnitVisualSettings>>();
            foreach (var setting in _unitVisualSettingsList)
            {
                if (setting == null)
                {
                    continue;
                }
                _visuals[setting.TeamType] = new Dictionary<int, UnitVisualSettings>
                {
                    [setting.UnitLevel] = setting
                };
            }
        }

        public UnitVisualSettings GetUnitVisualSettings(TeamType teamType, int unitLevel)
        {
            if (_visuals.ContainsKey(teamType) == false || _visuals[teamType].ContainsKey(unitLevel) == false)
            {
                return null;
            }

            return _visuals[teamType][unitLevel];
        }
    }
}