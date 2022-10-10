using NotFound.Utils;
using UnityEngine;

namespace NotFound
{

    public class TeamManager : MonoSingleton<TeamManager>
    {
        [SerializeField] private TeamData FootballHooligansTeamData;
        [SerializeField] private TeamData SoccerHooligansTeamData;
        
        private TeamType _userTeamSide = TeamType.Soccer;
        private TeamType _aiTeamSide = TeamType.Football;

        public void SetTeamSideFootball()
        {
            _userTeamSide = TeamType.Football;
            _aiTeamSide = TeamType.Soccer;
        }

        public void SetTeamSideSoccer()
        {
            _userTeamSide = TeamType.Soccer;
            _aiTeamSide = TeamType.Football;
        }

        public TeamType GetOwnTeamType()
        {
            return _userTeamSide;
        }

        public TeamType GetOpponentTeamType()
        {
            return _aiTeamSide;
        }

        public TeamData GetOwnTeamData()
        {
            return _userTeamSide == TeamType.Football ? FootballHooligansTeamData : SoccerHooligansTeamData;
        }
        
        public TeamData GetOpponentTeamData()
        {
            return _userTeamSide == TeamType.Football ? SoccerHooligansTeamData : FootballHooligansTeamData;
        }
    }
}
