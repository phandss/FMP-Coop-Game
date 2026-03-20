using UnityEngine;

public class InstantKillTrap : TrapBase
{
    [SerializeField] private HumanHealth health;
    public override void Activate()
    {
        
        if (health != null)
        {
            health.TakeDamage(health.maxHealth);
        }
    }
}
