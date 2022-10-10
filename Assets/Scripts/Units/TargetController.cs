using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using UnityEngine.AI;

public class TargetController : MonoBehaviour
{
    
    [SerializeField] private Hooligan _owner;
    [SerializeField] private NavMeshAgent _agent;
    
    public GameObject Target { get; private set; }
    public float Distance { get; private set; }

    private readonly List<Hooligan> _possibleTargets = new();

    private bool _isTargettingNode;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.transform.parent == null)
        {
            return;
        }
        Hooligan hooligan = other.transform.parent.GetComponent<Hooligan>();
        
        if (hooligan != null && _owner.TeamIndex != hooligan.TeamIndex)
        {
            if (_possibleTargets.Contains(hooligan) == false)
            {
                _possibleTargets.Add(hooligan);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.transform.parent == null)
        {
            return;
        }
        Hooligan hooligan = other.transform.parent.GetComponent<Hooligan>();
        
        if (hooligan != null && _owner.TeamIndex != hooligan.TeamIndex)
        {
            if (_possibleTargets.Contains(hooligan))
            {
                _possibleTargets.Remove(hooligan);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTargettingNode == false)
        {
            Hooligan closestHooligan = GetClosestEnemy(_possibleTargets.Where(x => x.Destroyed == false).ToList());

            if (closestHooligan != null)
            {
                Target = closestHooligan.gameObject;
            }
        }

        if (Target == null || Target.GetComponent<Hooligan>() != null && Target.GetComponent<Hooligan>().Destroyed)
        {
            if (Target == null)
            {
                return;
            }
            
            Hooligan hooligan = Target.GetComponent<Hooligan>();
            
            if (_possibleTargets.Contains(hooligan))
            {
                _possibleTargets.Remove(hooligan);
            }
            
            _isTargettingNode = false;

            return;
        }

        var targetPosition = Target.transform.position;
        targetPosition.y = 0;
        
        _agent.destination = targetPosition;
        Distance = Vector3.Distance(targetPosition, transform.position);
    }

    private Hooligan GetClosestEnemy(IList<Hooligan> enemies)
    {
        Hooligan closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Hooligan potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestEnemy = potentialTarget;
            }
        }

        return closestEnemy;
    }

    public void SetNodeTarget(GameObject targetNode)
    {
        if (targetNode == null)
        {
            return;
        }
        
        Target = targetNode;
        Distance = Vector3.Distance(Target.transform.position, this.transform.position);
        _isTargettingNode = true;
    }

    public void ResetTarget()
    {
        Target = null;
        _isTargettingNode = false;
    }

    public bool ShouldInteractWithNode(GameObject node)
    {
        return _isTargettingNode && node == Target;
    }

    public bool IsBusy()
    {
        return _isTargettingNode || Target != null;
    }
}

