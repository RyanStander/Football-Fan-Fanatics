using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(this.gameObject);
    }

    public void ProcessHitDamage(int damage)
    {
        this.health -= damage;
    }
}
