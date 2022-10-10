using Units;
using UnityEngine;

public class NodeCollissionController : MonoBehaviour
{
    [SerializeField] private NodeUpgradeController nodeUpgradeController;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.transform.parent == null)
        {
            return;
        }
        
        Hooligan hooligan = other.transform.parent.GetComponent<Hooligan>();

        if (hooligan == null)
        {
            return;
        }
        
        if (hooligan.Destroyed == false && hooligan.TargetController.ShouldInteractWithNode(gameObject))
        {
            if (nodeUpgradeController.AdjustStamina(hooligan.Damage, hooligan.TeamIndex))
            {
                hooligan.DestroyUnit();
            }
        }
    }
}
