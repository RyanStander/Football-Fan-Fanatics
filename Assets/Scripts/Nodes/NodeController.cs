using System;
using NotFound;
using UnityEngine;

public enum NodeState
{
    Neutral,
    Friendly,
    Hostile
}

public enum NodeType
{
    Base,
    Outpost
}

public class NodeController : MonoBehaviour
{
    [SerializeField] private NodeUpgradeController _nodeUpgradeController;
    [SerializeField] private NodeCollissionController _nodeCollissionController;
    [SerializeField] private int _teamIndex;
    [SerializeField] private NodeType _nodeType;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _neutralSprite;

    private void Start()
    {
        UpdateTeamMaterial();
    }

    public NodeState GetNodeState()
    {
        return _teamIndex switch
        {
            -1 => NodeState.Neutral,
            0 => NodeState.Friendly,
            _ => NodeState.Hostile
        };
    }

    public int GetTeamIndex()
    {
        return _teamIndex;
    }

    public void SwapOwnership(int newOwnerTeamIndex)
    {
        _teamIndex = newOwnerTeamIndex;

        UpdateTeamMaterial();
    }

    private void UpdateTeamMaterial()
    {
        _spriteRenderer.sprite = GetNodeState() switch
        {
            NodeState.Neutral => _neutralSprite,
            NodeState.Friendly => TeamManager.Instance.GetOwnTeamData().TeamBaseSprite,
            NodeState.Hostile => TeamManager.Instance.GetOpponentTeamData().TeamBaseSprite,
        };
    }

    public NodeType GetNodeType()
    {
        return _nodeType;
    }

    public int GetCurrentLevel()
    {
        return _nodeUpgradeController.GetCurrentLevel();
    }

    public NodeCollissionController GetCollisionController()
    {
        return _nodeCollissionController;
    }
}
