using NotFound;
using Units;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private NodeController _nodeController;
    [SerializeField] private Transform _spawnPosition;
    
    public float spawnTimerSetting;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimerSetting;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nodeController.GetNodeState() == NodeState.Neutral)
        {
            return;
        }
        
        spawnTimer -= Time.deltaTime;

        if (spawnTimer > 0)
            return;

        SpawnUnits();
    }

    void SpawnUnits()
    {
        spawnTimer = spawnTimerSetting;
        var spawnAmount = _nodeController.GetCurrentLevel();

        var teamType = _nodeController.GetNodeState() == NodeState.Friendly ? TeamManager.Instance.GetOwnTeamType() : TeamManager.Instance.GetOpponentTeamType();

        if (_nodeController.GetNodeType() == NodeType.Base)
        {
            spawnAmount *= 2;
        }
        
        for (int i = 0; i < spawnAmount; i++)
        {
            var position = _spawnPosition.position + Random.insideUnitSphere*0.5f;
            position.y = .5f;
            var unit = Instantiate(_nodeController.GetNodeState() == NodeState.Hostile ? 
                TeamManager.Instance.GetOpponentTeamData().TeamUnitPrefab : 
                TeamManager.Instance.GetOwnTeamData().TeamUnitPrefab, 
                position, 
                Quaternion.identity);
            unit.GetComponent<Hooligan>().SetTeam(_nodeController.GetTeamIndex(), teamType , 1);
        }
    }
}