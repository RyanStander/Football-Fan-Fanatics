using UnityEngine;

public class NodeUpgradeController : MonoBehaviour
{
    [SerializeField] private NodeController _nodeController;
    [SerializeField] private int _baseStamina = 2;
    [SerializeField] private BaseSoundPlayer _baseSoundPlayer;
    [SerializeField] private int _currentLevel = 1;
    
    private int _lastAdjustedTeamIndex;
    private int _currentStamina;

    private int MAX_LEVEL = 3;

    private void Awake()
    {
        _currentStamina = GetBaseStaminaForLevel(_currentLevel);
    }

    public bool AdjustStamina(int stamina, int teamIndex)
    {
        _lastAdjustedTeamIndex = teamIndex;

        var isStaminaAdjusted = false;
        
        if (_nodeController.GetTeamIndex() == teamIndex)
        {
            if (_currentLevel < MAX_LEVEL)
            {
                _currentStamina += stamina;
                isStaminaAdjusted = true;
            }
        }
        else
        {
            _currentStamina -= stamina;
            isStaminaAdjusted = true;
        }

        CheckNodeStatus();

        return isStaminaAdjusted;
    }

    private void CheckNodeStatus()
    {
        if (_currentStamina <= 0)
        {
            _nodeController.SwapOwnership(_lastAdjustedTeamIndex);
            //check which team it swaps to
            switch (_lastAdjustedTeamIndex)
            {
                //neutral
                case -1:
                    _baseSoundPlayer.PlayLostClip();
                    break;
                //friendly
                case 0:
                    _baseSoundPlayer.PlayLostClip();
                    break;
                //hostile
                case 1:
                    _baseSoundPlayer.PlayCaptureClip();
                    break;
            }
            
            _currentStamina = GetBaseStaminaForLevel(_currentLevel);

            return;
        }

        if (_currentStamina >= GetBaseStaminaForLevel(_currentLevel + 1))
        {
            _currentLevel++;
            _baseSoundPlayer.PlayUpgradeClip();

            _currentStamina = GetBaseStaminaForLevel(_currentLevel);
        }
    }

    private int GetBaseStaminaForLevel(int level)
    {
        return _baseStamina * level;
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }
}
