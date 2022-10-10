using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    [SerializeField] private int unitCountMultiplierToAttackBase=2;
    public int MinDecisionWaitTime = 3;
    public int MaxDecisionWaitTime = 7;

    private float _nextWaitTime;

    public List<NodeController> bases;
    public List<NodeController> outposts;

    private void Awake()
    {
        _nextWaitTime = RandomNumberGenerator.GetInt32(MinDecisionWaitTime, MaxDecisionWaitTime);
    }

    private void Update()
    {
        _nextWaitTime -= Time.deltaTime;

        if (bases.Any() == false)
        {
            return;
        }

        if (_nextWaitTime > 0)
        {
            return;
        }

        var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        var allHooligans = rootGameObjects.Where(x => x.GetComponent<Hooligan>() != null).ToArray();
        var ownHooligans = allHooligans.Where(x => x.GetComponent<Hooligan>().TeamIndex == 1).ToArray();
        var opponentHooligans = allHooligans.Where(x => x.GetComponent<Hooligan>().TeamIndex != 1).ToArray();

        if (ownHooligans.Any() == false || bases.Any() == false)
        {
            return;
        }

        //if ai hooligan count is smaller than a value of player hooligans
        if (ownHooligans.Count() <= (opponentHooligans.Count() * unitCountMultiplierToAttackBase))
        {
            TargetOutposts(ownHooligans);
        }
        else
        {
            var target = bases.FirstOrDefault(x => x.GetTeamIndex() != 1);

            if (target==null)
            {
                TargetOutposts(ownHooligans);
            }
            else
            {
                foreach (var ownHooligan in ownHooligans)
                {
                    ownHooligan.GetComponentInChildren<TargetController>().SetNodeTarget(target.GetCollisionController().gameObject);
                }
            }
        }

        _nextWaitTime = RandomNumberGenerator.GetInt32(MinDecisionWaitTime, MaxDecisionWaitTime);
    }

    private void TargetOutposts(GameObject[] ownHooligans)
    {
        if (outposts.Any() == false)
        {
            return;
        }
            
        var eligibleOutposts = outposts.Where(x => x.GetTeamIndex() == -1).ToArray();

        if (eligibleOutposts.Any() == false)
        {
            eligibleOutposts = outposts.Where(x => x.GetTeamIndex() != 1).ToArray();
        }

        if (eligibleOutposts.Any())
        {
            var randomEntry = new System.Random().Next(eligibleOutposts.Count());
            var chosenOutpost = eligibleOutposts[randomEntry];

            foreach (var ownHooligan in ownHooligans.Where(x => x.GetComponentInChildren<TargetController>().IsBusy() == false))
            {
                ownHooligan.GetComponentInChildren<TargetController>().SetNodeTarget(chosenOutpost.GetCollisionController().gameObject);
            }
        }
    }
}